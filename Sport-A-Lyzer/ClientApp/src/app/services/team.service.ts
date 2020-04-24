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

  putPlayer(playerId, data) {
    return this.http.put(`/api/players/${playerId}`, data);
  }

  putTeam(teamId, data) {
    return this.http.put(`/api/teams/${teamId}`, data);
  }
}
