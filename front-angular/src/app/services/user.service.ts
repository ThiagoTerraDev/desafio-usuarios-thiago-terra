import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/response';
import { User, CreateUserRequest, UpdateUserRequest } from '../models/user';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private apiUrl = `${environment.ApiUrl}/users`;

  constructor(private http: HttpClient) { }

  GetUsers() : Observable<Response<User[]>> {
    return this.http.get<Response<User[]>>(this.apiUrl);
  }

  GetUserById(id: number) : Observable<Response<User>> {
    return this.http.get<Response<User>>(`${this.apiUrl}/${id}`);
  }

  CreateUser(user: CreateUserRequest) : Observable<Response<User>> {
    return this.http.post<Response<User>>(this.apiUrl, user);
  }

  EditUser(user: UpdateUserRequest) : Observable<Response<User>> {
    return this.http.put<Response<User>>(this.apiUrl, user);
  }

  DeactivateUser(id: number) : Observable<Response<User>> {
    return this.http.patch<Response<User>>(`${this.apiUrl}/${id}/deactivate`, {});
  }

  DeleteUser(id: number) : Observable<Response<User>> {
    return this.http.delete<Response<User>>(`${this.apiUrl}/${id}`);
  }
}

