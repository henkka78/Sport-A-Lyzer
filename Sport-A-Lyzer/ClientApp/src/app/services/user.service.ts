import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from "../models/user.model";
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.baseUri;
  }

  getAll() {
    return this.http.get<User[]>(`${this.baseUrl}/api/users`);
  }

  addUser(user: User) {
    return this.http.post(`${this.baseUrl}/api/users/register`, user);
  }
}
