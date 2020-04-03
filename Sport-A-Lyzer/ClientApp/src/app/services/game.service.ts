import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../models/game.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getGamesByTournamentId(tournamentId: string): Observable<Game[]> {
    return this.httpClient.get<Game[]>(`/api/tournaments/${tournamentId}/games`);
  }

  upsertGame(gameId: string, data: any) {
    return this.httpClient.post(`/api/games/${gameId}`, data);
  }
}
