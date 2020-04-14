import { Component, OnInit, ViewChild } from '@angular/core';
import { GameService } from '../../services/game.service';
import { TeamService } from '../../services/team.service';
import { Game } from '../../models/game.model';
import { Team } from '../../models/team.model';
import { forkJoin } from 'rxjs';
import { ModalDirective } from 'ng-uikit-pro-standard';

@Component({
  selector: 'app-game-admin',
  templateUrl: './game-admin.component.html',
  styleUrls: ['./game-admin.component.scss']
})
export class GameAdminComponent implements OnInit {
  @ViewChild('gameModal', { static: false }) gameEditorModal: ModalDirective;

  public games: Game[];
  public teams: Team[];
  public teamsSelect: any[];
  public teamsLoaded: boolean;
  public gameModalOpened: boolean;

  constructor(
    private gameService: GameService,
    private teamService: TeamService
  ) { }

  ngOnInit() {
    const gamesQuery = this.gameService.getGamesByTournamentId();
    const teamsQuery = this.teamService.getTeams();
    forkJoin(gamesQuery, teamsQuery).subscribe(results => {
      this.games = results[0];
      this.teams = results[1];
      this.teamsSelect = [];
      this.teams.forEach(team => {
        let teamObject = {
          value: team.teamId,
          label: team.name
        }
        this.teamsSelect.push(teamObject);
        this.teamsLoaded = true;
      });
    });
  }

  public addGame() {
    this.gameModalOpened = true;
    this.gameEditorModal.show();
  }

}
