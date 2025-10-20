import { Component } from '@angular/core';
import { UserForm } from "../../components/user-form/user-form";
import { CreateUserRequest } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  imports: [UserForm],
  templateUrl: './create.html',
  styleUrl: './create.css'
})
export class Create {

  formTitle: string = 'Criar Usuário';
  btnAction: string = 'Criar';

  constructor(private userService: UserService, private router: Router) { }

  createUser(user: any) {
    const createUserDto: CreateUserRequest = {
      name: user.name,
      lastName: user.lastName,
      email: user.email,
      password: user.password,
      department: user.department,
      shift: user.shift
    };

    this.userService.CreateUser(createUserDto).subscribe({
      next: (response) => {
        alert('Usuário criado com sucesso!');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        console.error('Error creating user:', error);
        const errorMessage = error.error?.message || 'Erro ao criar usuário. Tente novamente.';
        alert(errorMessage);
      },
      complete: () => {
        console.log('Request to create user completed.');
      }
    });
  }
}
