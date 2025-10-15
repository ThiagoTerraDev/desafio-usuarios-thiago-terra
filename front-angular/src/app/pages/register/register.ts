import { Component } from '@angular/core';
import { UserForm } from "../../components/user-form/user-form";
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [UserForm],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  btnTitle: string = 'Cadastrar Usuário';
  btnAction: string = 'Cadastrar';

  constructor(private userService: UserService, private router: Router) { }

  createUser(user: User) {
    this.userService.CreateUser(user).subscribe({
      next: (response) => {
        alert('Usuário criado com sucesso!');
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Error creating user:', error);
        alert('Error creating user. Please try again.');
      },
      complete: () => {
        console.log('Request to create user completed.');
      }
    });
  }
}
