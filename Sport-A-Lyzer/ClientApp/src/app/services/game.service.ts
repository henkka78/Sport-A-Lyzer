import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../models/game.model';
import { Goal } from '../models/goal.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getGamesByTournamentId(tournamentId: string=""): Observable<Game[]> {
    return this.httpClient.get<Game[]>(`/api/games`);
  }

  upsertGame(gameId: string, data: any) {
    return this.httpClient.put(`/api/games/${gameId}`, data);
  }

  getGame(gameId: string): Observable<Game> {
    return this.httpClient.get<Game>(`/api/games/${gameId}`);
  }

  startGame(gameId: string, request: any) {
    return this.httpClient.post(`/api/games/${gameId}/start`, request);
  }

  endGame(gameId: string, request: any) {
    return this.httpClient.post(`/api/games/${gameId}/end`, request);
  }

  setGamePauseStatus(gameId: string, request: any) {
    return this.httpClient.post(`/api/games/${gameId}/set-pause-status`, request);
  }

  putGoal(goalId: string, request: any) {
    return this.httpClient.put(`/api/goal/${goalId}`, request);
  }
}
