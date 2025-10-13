import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/response';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private apiUrl = `${environment.ApiUrl}/users`;

  constructor(private http: HttpClient) { }

  GetUsers() : Observable<Response<User[]>> {
    return this.http.get<Response<User[]>>(this.apiUrl);
  }

}

