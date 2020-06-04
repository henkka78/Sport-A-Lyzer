import { Component, OnInit, ViewChild } from '@angular/core';
import { formatDate } from '@angular/common';
import { GameService } from '../../services/game.service';
import { Game } from '../../models/game.model';
import { timer, Subject } from 'rxjs';
import { Goal } from "../../models/goal.model";
import { ModalDirective, ToastService } from 'ng-uikit-pro-standard';
import { UUID } from 'angular2-uuid';
import { Router, ActivatedRoute } from "@angular/router";
import { FormGroup, FormControl } from '@angular/forms';
import { ClipboardService } from 'ngx-clipboard';
import { DeviceDetectorService } from 'ngx-device-detector';
import { UserService } from "../../services/user.service";
import { AuthenticationService } from "../../services/authentication.service";


@Component({
  selector: 'app-game-follow',
  templateUrl: './game-follow.component.html',
  styleUrls: ['./game-follow.component.scss']
})
export class GameFollowComponent implements OnInit {

  @ViewChild('scorerModal') scorerModal: ModalDirective;
  //Käyttöliittymän muoto ja toiminta hakevat vielä uriaan. Muuttujaviidakko on tässä versiossa hieman sekava ja turhan tiheä. Sieventyy, kunhan ehtii...
  public minutes: number = 0;
  public seconds: number = 0;
  public secondsString: string;
  private secondsPlayed = 0;
  public showTimer: boolean;
  public gameTimer = timer(0, 1000);
  public gamePaused: boolean;
  public gameOn: boolean;
  public gameEnded: boolean;
  public timerSubscription;
  public games: any[];
  public endedGames: any[];
  public gamesLoaded: boolean;
  public gameSelected: boolean;
  public currentGame: Game;
  public scoredGoal: Goal;
  public goalScored: boolean;
  private focusLost: boolean;
  private isMobile: boolean;
  public optionsSet: boolean;
  private organizationOptions: any;
  public startClockSubject: Subject<void> = new Subject<void>();
  public gameYearSelections: any = {
    months: [
      {
        value: 1,
        label: "Tammikuu"
      },
      {
        value: 2,
        label: "Helmikuu"
      },
      {
        value: 3,
        label: "Maaliskuu"
      },
      {
        value: 4,
        label: "Huhtikuu"
      },
      {
        value: 5,
        label: "Toukokuu"
      },
      {
        value: 6,
        label: "Kesäkuu"
      },
      {
        value: 7,
        label: "Heinäkuu"
      },
      {
        value: 8,
        label: "Elokuu"
      },
      {
        value: 9,
        label: "Syyskuu"
      },
      {
        value: 10,
        label: "Lokakuu"
      },
      {
        value: 11,
        label: "Marraskuu"
      },
      {
        value: 12,
        label: "Joulukuu"
      }
    ],
    years: []
  };
  private gamesReguest = {
    Year: null,
    Month:null
  };

  public scorer: any = {
    scorerId: null,
    scorerName: null
  };
  public goals = {
    home: 0,
    away: 0,
    homeStats: {
      scorers: null
    },
    awayStats: {
      scorers: null
    }
  };
  public saving: boolean;

  public scorerSet: boolean;
  public gameSelectForm: FormGroup;
  constructor(
    private gameService: GameService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private clipboardService: ClipboardService,
    private toastService: ToastService,
    private deviceService: DeviceDetectorService,
    private authService: AuthenticationService
  ) { }

  ngOnInit() {
    this.isMobile = this.deviceService.isMobile();
    this.organizationOptions = JSON.parse(this.authService.currentUserValue.organizationOptions);
    console.log(this.organizationOptions);
    this.setYears(this.organizationOptions);
    console.log(this.gameYearSelections);
    //this.loadGameList();
  }

  private setYears(options: any): void {
    for (let i = 0; i < options.yearsToShow; i++) {
      this.gameYearSelections.years.push({
        value: options.firstUsageYear + i,
        label: options.firstUsageYear + i
      });
    }
    this.optionsSet = true;
  }

  public yearSelect(event): void {
    this.gamesReguest.Year = event.value;
    if (this.gamesReguest.Month != null) {
      this.loadGameList();
    }
  }

  public monthSelect(event): void {
    this.gamesReguest.Month = event.value;
    if (this.gamesReguest.Year != null) {
      this.loadGameList();
    }
  }

  private loadGameList(): void {
    this.gameService.getNonTournamentGamesByTimeLimit(this.gamesReguest).subscribe(results => {
      this.games = [];
      this.endedGames = [];
      results.forEach(result => {

        let gameObject = {
          value: result.id,
          label: (result.gameDay != null ? formatDate(result.gameDay, "d.M", "en-EN") + "  :  " : "") + result.homeTeamName + " - " + result.awayTeamName
        };
        if (result.actualEndTime === null) {
          this.games.push(gameObject);
        } else {
          this.endedGames.push(gameObject);
        }

      });
      var queryParamsGameId = this.activatedRoute.snapshot.queryParams.selectedGameId;
      if (queryParamsGameId !== null && queryParamsGameId !== undefined) {
        this.loadGame(queryParamsGameId, true);
        this.gameSelectForm = new FormGroup({
          gameSelect: new FormControl(queryParamsGameId)
        });
      } else {
        this.gameSelectForm = new FormGroup({
          gameSelect: new FormControl()
        });
      }
      this.gamesLoaded = true;
    });
  }

  public gameSelect(event) {
    this.router.navigate([],
      {
        relativeTo: this.activatedRoute,
        queryParams: {
          selectedGameId: event.value
        },
        queryParamsHandling: "merge"
      });
    this.loadGame(event.value, true);
  }

  public exitGame(): void {
    this.gameSelected = false;
    this.gamesLoaded = false;
    this.router.navigate([],
      {
        relativeTo: this.activatedRoute,
        queryParams: { selectedGameId: null },
        queryParamsHandling: "merge"
      });
    this.loadGameList();
  }

  public copyLink(): void {
    this.clipboardService.copyFromContent("http://www.bloodyhanks.com/game/" + this.currentGame.id);
    const options = { opacity: 1 };
    this.toastService.success("", "Linkki kopioitu leikepöydälle!", options);
  }

  private loadGame(gameId: string, setClock: boolean): void {
    this.gameService.getGame(gameId).subscribe(result => {
      this.resetGame(false);
      this.currentGame = result;
      this.gameEnded = this.currentGame.actualEndTime !== null;
      if (this.currentGame.actualStartTime !== null && this.currentGame.actualEndTime === null) {
        if (setClock) {

          this.setClock(this.currentGame.secondsPlayed);
        }


        this.gameOn = true;
      }
      this.goals.home = this.countGoals(this.currentGame.homeTeamId);
      this.goals.away = this.countGoals(this.currentGame.awayTeamId);
      if (this.currentGame.goalStats !== null) {
        this.goals.homeStats = this.currentGame.goalStats.find(gs => gs.teamId === this.currentGame.homeTeamId);
        this.goals.awayStats = this.currentGame.goalStats.find(gs => gs.teamId === this.currentGame.awayTeamId);
      }
      this.gameSelected = true;


      this.saving = false;
      this.scorerModal.hide();
    });
  }

  private setClock(secondsPlayed: number): void {
    this.secondsPlayed = secondsPlayed;
    this.minutes = Math.floor(this.secondsPlayed / 60);
    this.seconds = this.secondsPlayed % 60;
    this.startClock();
  }

  private resetGame(stopClock: boolean = false) {
    this.gameOn = false;
    this.gameSelected = false;
    this.goals = {
      home: 0,
      away: 0,
      homeStats: {
        scorers: null
      },
      awayStats: {
        scorers: null
      }
    };

    if (this.timerSubscription && stopClock) {
      this.timerSubscription.unsubscribe();
      this.secondsPlayed = 0;
      this.minutes = 0;
      this.seconds = 0;
    }
  }

  private countGoals(teamId: string): number {
    if (this.currentGame.goalStats == null) {
      return 0;
    }
    var teamsGoalStats = this.currentGame.goalStats.find(gs => gs.teamId === teamId);
    if (teamsGoalStats == null) {
      return 0;
    }
    var goals = 0;
    teamsGoalStats.scorers.forEach(scorer => {
      goals += scorer.numberOfGoals;
    });
    return goals;

  }
  public goal(teamId: string): void {
    this.scoredGoal = {
      minuteOfGame: this.minutes + 1,
      teamId: teamId,
      goalTypeId: 1,
      playerId: null,
      gameId: this.currentGame.id

    };
    this.goalScored = true;
    this.scorer = {
      scorerId: null,
      scorerName: null
    };
    this.scorerModal.show();
  }

  public setScorer(id: string): void {
    let player = this.currentGame.players[this.scoredGoal.teamId].find(p => p.playerId === id);
    this.scorer.scorerId = id;
    this.scorer.scorerName = player.playerNumber + " - " + player.firstName + " " + player.lastName;
    this.scoredGoal.playerId = id;
    this.scorerSet = true;
  }

  public saveGoal() {
    this.saving = true;
    this.gameService.putGoal(UUID.UUID(), this.scoredGoal).subscribe(() => {
      this.loadGame(this.currentGame.id, false);
    });

  }

  public startGame() {
    this.gameService.startGame(this.currentGame.id, { StartTime: new Date() }).subscribe(() => {
    });
    this.gameOn = true;
    this.startClock();
  }

  public endGame() {
    this.gameEnded = true;
    this.gameService.endGame(this.currentGame.id, { EndTime: new Date() }).subscribe(() => {
      this.loadGame(this.currentGame.id, false);
    });
    this.timerSubscription.unsubscribe();
  }

  public pauseGame() {
    if (this.gamePaused) {
      this.gameService.setGamePauseStatus(this.currentGame.id, { eventTimeStamp: new Date(), isActivePause: true })
        .subscribe(() => this.loadGame(this.currentGame.id, false));
      this.gamePaused = false;

      this.startClock();
    } else {
      this.gameService.setGamePauseStatus(this.currentGame.id, { eventTimeStamp: new Date(), isActivePause: false })
        .subscribe(() => console.log("Paused"));
      this.timerSubscription.unsubscribe();
      this.gamePaused = true;
    }
  }

  private startClock() {
    this.startClockSubject.next();
  }
}
