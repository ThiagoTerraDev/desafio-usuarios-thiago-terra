import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Response } from '../models/response';
import { AuthResponse, LoginRequest, SignupRequest } from '../models/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.ApiUrl}/auth`;
  private currentUserSubject: BehaviorSubject<AuthResponse | null>;
  public currentUser$: Observable<AuthResponse | null>;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    const storedUser = localStorage.getItem('currentUser');
    this.currentUserSubject = new BehaviorSubject<AuthResponse | null>(
      storedUser ? JSON.parse(storedUser) : null
    );
    this.currentUser$ = this.currentUserSubject.asObservable();
  }

  // Retorna o usuário atual
  public get currentUserValue(): AuthResponse | null {
    return this.currentUserSubject.value;
  }

  // Retorna o token JWT
  public get token(): string | null {
    return this.currentUserValue?.token || null;
  }

  public get isLoggedIn(): boolean {
    return !!this.currentUserValue;
  }

  signup(request: SignupRequest): Observable<Response<AuthResponse>> {
    return this.http.post<Response<AuthResponse>>(`${this.apiUrl}/register`, request)
      .pipe(
        tap(response => {
          if (response.success && response.data) {
            this.setCurrentUser(response.data);
          }
        })
      );
  }

  login(request: LoginRequest): Observable<Response<AuthResponse>> {
    return this.http.post<Response<AuthResponse>>(`${this.apiUrl}/login`, request)
      .pipe(
        tap(response => {
          if (response.success && response.data) {
            this.setCurrentUser(response.data);
          }
        })
      );
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  forgotPassword(request: { email: string }): Observable<Response<string>> {
    return this.http.post<Response<string>>(`${this.apiUrl}/forgot-password`, request);
  }

  resetPassword(request: { token: string; password: string; confirmPassword: string }): Observable<Response<boolean>> {
    return this.http.post<Response<boolean>>(`${this.apiUrl}/reset-password`, request);
  }

  private setCurrentUser(user: AuthResponse): void {
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }
}
