import { Component, OnInit, ViewChild } from '@angular/core';
import { GameService } from '../../services/game.service';
import { Game } from '../../models/game.model';
import { timer } from 'rxjs';
import { Goal } from "../../models/goal.model";
import { ModalDirective } from 'ng-uikit-pro-standard';
import { UUID } from 'angular2-uuid';

@Component({
  selector: 'app-game-follow',
  templateUrl: './game-follow.component.html',
  styleUrls: ['./game-follow.component.scss']
})
export class GameFollowComponent implements OnInit {
  @ViewChild('scorerModal', { static: false }) scorerModal: ModalDirective;

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
  public gamesLoaded: boolean;
  public gameSelected: boolean;
  public currentGame: Game;
  public scoredGoal: Goal;
  public goalScored: boolean;
  public scorer: any = {
    scorerId: null,
    scorerName: null
  }
  public goals = {
    home: 0,
    away: 0,
    homeStats: [],
    awayStats: []
  };
  public saving: boolean;

  public scorerSet: boolean;

  constructor(
    private gameService: GameService
  ) { }

  ngOnInit() {
    this.gameService.getGamesByTournamentId().subscribe(results => {
      console.log(results);
      this.games = [];
      results.forEach(result => {
        let gameObject = {
          value: result.id,
          label: result.homeTeamName + " - " + result.awayTeamName
        }
        this.games.push(gameObject);
        this.gamesLoaded = true;
      });
    });
  }

  public gameSelect(event) {
    this.loadGame(event.value);
  }

  private loadGame(gameId) {
    this.gameService.getGame(gameId).subscribe(result => {
      this.resetGame();
      this.currentGame = result;
      if (this.currentGame.actualStartTime !== null && this.currentGame.actualEndTime === null) {
        this.secondsPlayed = Math.floor((new Date().getTime() - new Date(this.currentGame.actualStartTime).getTime()) / 1000);
        console.log(new Date().toLocaleString());
        this.minutes = Math.floor(this.secondsPlayed / 60);
        this.seconds = this.secondsPlayed % 60;
        this.startClock();
        this.gameOn = true;
      }
      this.goals.home = this.countGoals(this.currentGame.homeTeamId);
      this.goals.away = this.countGoals(this.currentGame.awayTeamId);
      if (this.currentGame.goalStats !== null) {
        this.goals.homeStats = this.currentGame.goalStats.find(gs => gs.teamId === this.currentGame.homeTeamId);
        this.goals.awayStats = this.currentGame.goalStats.find(gs => gs.teamId === this.currentGame.awayTeamId);
      }
      this.gameSelected = true;
      console.log(this.currentGame);
      this.saving = false;
      this.scorerModal.hide();
    });
  }

  private resetGame() {
    this.secondsPlayed = 0;
    this.minutes = 0;
    this.seconds = 0;
    this.gameOn = false;
    this.gameSelected = false;
    this.goals = {
      home: 0,
      away: 0,
      homeStats: [],
      awayStats: []
    };
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
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

    }
    this.goalScored = true;
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
      this.timerSubscription.unsubscribe();
      this.loadGame(this.currentGame.id);
    });

    console.log(this.scoredGoal);
  }
  public startGame() {
    this.gameService.startGame(this.currentGame.id, { StartTime: new Date() }).subscribe(() => {
      console.log("Started!");
    });
    this.gameOn = true;
    this.startClock();
  }

  public endGame() {
    this.gameEnded = true;
    this.gameService.endGame(this.currentGame.id, { EndTime: new Date() }).subscribe(() => {
      console.log("Ended!");
    });
    this.timerSubscription.unsubscribe();
  }

  public pauseGame() {
    if (this.gamePaused) {
      this.gamePaused = false;
      this.startClock();
    } else {
      this.timerSubscription.unsubscribe();
      this.gamePaused = true;
    }
  }

  private startClock() {
    this.timerSubscription = this.gameTimer.subscribe(value => {
      this.secondsPlayed++;
      this.seconds++;
      if (this.seconds === 60) {
        this.minutes++;
        this.seconds = 0;
      }
      this.secondsString = this.seconds.toString().padStart(2, "0");
    });
  }
}
