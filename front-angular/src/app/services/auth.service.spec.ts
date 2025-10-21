import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { provideZoneChangeDetection } from '@angular/core';
import { AuthService } from './auth.service';
import { AuthResponse } from '../models/auth';

describe('AuthService', () => {
  let service: AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideZoneChangeDetection({ eventCoalescing: true }),
        AuthService,
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([])
      ]
    });
    service = TestBed.inject(AuthService);
    localStorage.clear();
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return false for isLoggedIn when no user is stored', () => {
    expect(service.isLoggedIn).toBeFalse();
  });

  it('should return null for token when no user is stored', () => {
    expect(service.token).toBeNull();
  });

  it('should clear user data on logout', () => {
    const mockUser: AuthResponse = {
      userId: 1,
      name: 'Test',
      lastName: 'User',
      email: 'test@test.com',
      token: 'fake-jwt-token'
    };

    localStorage.setItem('currentUser', JSON.stringify(mockUser));

    // Atualiza o BehaviorSubject manualmente
    service['currentUserSubject'].next(mockUser);

    expect(service.isLoggedIn).toBeTrue();

    service.logout();

    expect(service.isLoggedIn).toBeFalse();
    expect(localStorage.getItem('currentUser')).toBeNull();
  });

  it('should load user from localStorage on initialization', () => {
    const mockUser: AuthResponse = {
      userId: 1,
      name: 'Test',
      lastName: 'User',
      email: 'test@test.com',
      token: 'fake-jwt-token'
    };

    // Seta o localStorage ANTES de criar o serviço
    localStorage.setItem('currentUser', JSON.stringify(mockUser));

    // Cria um novo TestBed com o localStorage já populado
    TestBed.resetTestingModule();
    TestBed.configureTestingModule({
      providers: [
        provideZoneChangeDetection({ eventCoalescing: true }),
        AuthService,
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([])
      ]
    });

    const newService = TestBed.inject(AuthService);

    expect(newService.currentUserValue).toEqual(mockUser);
    expect(newService.token).toBe('fake-jwt-token');
    expect(newService.isLoggedIn).toBeTrue();
  });
});
