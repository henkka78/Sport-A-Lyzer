import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../models/game.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private baseUrl:string;
  constructor(
    private httpClient: HttpClient
  ) {
    this.baseUrl = environment.baseUri;
  }

  getGamesByTournamentId(tournamentId: string=""): Observable<Game[]> {
    return this.httpClient.get<Game[]>(`${this.baseUrl}/api/games`);
  }

  getNonTournamentGamesByTimeLimit(request: any): Observable<Game[]> {
    return this.httpClient.get<Game[]>(`${this.baseUrl}/api/games/non-tournament-games-by-time-limit?year=${request.Year}&month=${request.Month}`);
  }

  upsertGame(gameId: string, data: any) {
    return this.httpClient.put(`${this.baseUrl}/api/games/${gameId}`, data);
  }

  getGame(gameId: string): Observable<Game> {
    return this.httpClient.get<Game>(`${this.baseUrl}/api/games/${gameId}`);
  }

  startGame(gameId: string, request: any) {
    return this.httpClient.post(`${this.baseUrl}/api/games/${gameId}/start`, request);
  }

  endGame(gameId: string, request: any) {
    return this.httpClient.post(`${this.baseUrl}/api/games/${gameId}/end`, request);
  }

  setGamePauseStatus(gameId: string, request: any) {
    return this.httpClient.post(`${this.baseUrl}/api/games/${gameId}/set-pause-status`, request);
  }

  putGoal(goalId: string, request: any) {
    return this.httpClient.put(`${this.baseUrl}/api/goal/${goalId}`, request);
  }
}
