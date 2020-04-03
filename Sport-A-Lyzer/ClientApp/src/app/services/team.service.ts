import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PlayerModel } from '../models/player.model'
import { Team } from '../models/team.model'

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  constructor(
    private http: HttpClient,
  ) { }

  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(`/api/teams`);
  }

  getTeamsPlayers(teamId: string): Observable<PlayerModel[]> {
    return this.http.get<PlayerModel[]>(`/api/teams/${teamId}/players`);
  }

  postPlayer(playerId, data) {
    return this.http.post(`/api/players/${playerId}`, data);
  }

  postTeam(teamId, data) {
    return this.http.post(`/api/teams/${teamId}`, data);
  }
}
