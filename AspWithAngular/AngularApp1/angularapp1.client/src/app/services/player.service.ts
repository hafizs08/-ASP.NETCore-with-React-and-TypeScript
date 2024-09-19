import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export interface Player {
  id: number;
  name: string;
  number: number;
  position: string;
}

export interface ApiResponse<T> {
  message: string;
  data: T;
}

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  private apiUrl = 'https://localhost:7132/api/players'; // Sesuaikan dengan URL API Anda

  constructor(private http: HttpClient) { }

  getPlayers(): Observable<ApiResponse<Player[]>> {
    return this.http.get<ApiResponse<Player[]>>(this.apiUrl);
  }

  getPlayerById(id: number): Observable<ApiResponse<Player>> {
    return this.http.get<ApiResponse<Player>>(`${this.apiUrl}/${id}`);
  }

  addPlayer(player: Player): Observable<ApiResponse<Player>> {
    return this.http.post<ApiResponse<Player>>(this.apiUrl, player);
  }

  updatePlayer(player: Player): Observable<ApiResponse<Player>> {
    return this.http.put<ApiResponse<Player>>(`${this.apiUrl}/${player.id}`, player);
  }

  deletePlayer(id: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.apiUrl}/${id}`);
  }
}
