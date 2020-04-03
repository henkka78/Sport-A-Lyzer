import { Component, OnInit, ViewChild } from '@angular/core';
import { TeamService } from '../../services/team.service';
import { PlayerModel } from '../../models/player.model'
import { Team } from '../../models/team.model'
import { ModalDirective } from 'ng-uikit-pro-standard';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.scss']
})
export class TeamComponent implements OnInit {
  public teams: Team[];
  public players: PlayerModel[];
  public editedPlayer: PlayerModel;
  public editedTeam: Team;
  public teamsLoaded = false;
  public playersLoaded = false;
  public selectedTeam: Team;
  public playerModalOpened = false;
  public teamModalOpened: boolean;
  public isNewPlayer: boolean;
  public isNewTeam: boolean;
  public playerModalHeader: string;

  @ViewChild('playerModal', { static: false }) playerEditorModal: ModalDirective;
  @ViewChild('teamModal', { static: false }) teamEditorModal: ModalDirective;

  constructor(
    private teamService: TeamService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.loadTeams();

  }

  public selectTeam(teamId): void {
    this.router.navigate([],
      {
        relativeTo: this.activatedRoute,
        queryParams: {
          selectedTeamId: teamId
        },
        queryParamsHandling: "merge"
      });
    this.selectedTeam = this.teams.find(t => t.teamId === teamId);
    this.loadPlayers();
  }

  private loadPlayers() {
    this.teamService.getTeamsPlayers(this.selectedTeam.teamId).subscribe(players => {
      this.players = players;
      this.playersLoaded = true;
    });
  }

  private loadTeams() {
    this.teamService.getTeams().subscribe(results => {
      this.teams = results;
      this.teamsLoaded = true;
      var queryParamsTeamId = this.activatedRoute.snapshot.queryParams.selectedTeamId;
      if (queryParamsTeamId !== null && queryParamsTeamId !== undefined) {
        this.selectedTeam = this.teams.find(t => t.teamId === queryParamsTeamId);
        this.loadPlayers();
      }

    });
  }

  public editorClose() {
    this.playerEditorModal.hide();
    this.playerModalOpened = false;
    this.loadPlayers();
  }

  public teamEditorClose() {
    this.teamEditorModal.hide();
    this.teamModalOpened = false;
    this.loadTeams();
  }

  public editPlayer(playerId: string): void {
    this.isNewPlayer = false;
    this.playerModalHeader = "Muokkaa pelaajan tietoja";
    this.editedPlayer = this.players.find(p => p.playerId === playerId);
    this.playerEditorModal.show();
    this.playerModalOpened = true;
  }

  public editTeam(teamId: string): void {
    this.isNewTeam = false;
    this.editedTeam = this.teams.find(t => t.teamId === teamId);
    this.teamEditorModal.show();
    this.teamModalOpened = true;
  }

  public addPlayer(): void {
    this.isNewPlayer = true;
    this.playerModalHeader = "Anna uuden pelaajan tiedot";
    this.editedPlayer = new PlayerModel();
    this.playerEditorModal.show();
    this.playerModalOpened = true;
  }

  public addTeam(): void {
    this.isNewTeam = true;
    this.editedTeam = new Team();
    this.teamEditorModal.show();
    this.teamModalOpened = true;
  }


}
