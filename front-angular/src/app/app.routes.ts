import { Routes } from '@angular/router';
import { Register } from './pages/register/register';
import { Home } from './pages/home/home';
import { Edit } from './pages/edit/edit';
import { Details } from './pages/details/details';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'register', component: Register },
  { path: 'edit/:id', component: Edit },
  { path: 'details/:id', component: Details }
];
