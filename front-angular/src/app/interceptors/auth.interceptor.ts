import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.token;

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      // Se receber 401 Unauthorized, faz logout automático
      if (error.status === 401 && authService.isLoggedIn) {
        console.warn('Token expirado ou inválido. Fazendo logout...');
        authService.logout();
      }
      return throwError(() => error);
    })
  );
};
