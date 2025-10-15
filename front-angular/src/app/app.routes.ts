import { Routes } from '@angular/router';
import { Register } from './pages/register/register';
import { Home } from './pages/home/home';
import { Edit } from './pages/edit/edit';

export const routes: Routes = [
  {
    path: '', component: Home
  },
  {
    path: 'register', component: Register
  },
  {
    path: 'edit/:id', component: Edit
  }
];
