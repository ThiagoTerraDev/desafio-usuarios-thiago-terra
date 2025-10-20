import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Signup } from './pages/signup/signup';
import { Dashboard } from './pages/dashboard/dashboard';
import { Create } from './pages/create/create';
import { Edit } from './pages/edit/edit';
import { Details } from './pages/details/details';
import { authGuard } from './guards/auth.guard';
import { publicGuard } from './guards/public.guard';

export const routes: Routes = [
  { path: 'login', component: Login, canActivate: [publicGuard] },
  { path: 'signup', component: Signup, canActivate: [publicGuard] },
  { path: 'dashboard', component: Dashboard, canActivate: [authGuard] },
  { path: 'create', component: Create, canActivate: [authGuard] },
  { path: 'edit/:id', component: Edit, canActivate: [authGuard] },
  { path: 'details/:id', component: Details, canActivate: [authGuard] },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
