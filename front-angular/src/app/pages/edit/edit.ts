import { Component, OnInit } from '@angular/core';
import { UserForm } from "../../components/user-form/user-form";
import { User } from '../../models/user';
import { UpdateUserRequest } from '../../models/auth';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  imports: [UserForm],
  templateUrl: './edit.html',
  styleUrl: './edit.css'
})
export class Edit implements OnInit{

  formTitle: string = 'Editar Usuário';
  btnAction: string = 'Editar';
  userData?: User;

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const userId = Number(this.route.snapshot.paramMap.get('id'));

    if (userId) {
      this.userService.GetUserById(userId).subscribe({
        next: (response) => {
          this.userData = response.data;
        },
        error: (error) => {
          console.error('Error fetching user data:', error);
          alert('Erro ao carregar dados do usuário.');
        },
        complete: () => {
          console.log('Request to fetch user data completed.');
        }
      });
    } else {
      alert('Não foi possível carregar os dados do usuário.');
      this.router.navigate(['/dashboard']);
    }
  }

  editUser(user: any): void {
    const updateUserDto: UpdateUserRequest = {
      id: user.id,
      name: user.name,
      lastName: user.lastName,
      email: user.email,
      department: user.department,
      shift: user.shift
    };

    this.userService.EditUser(updateUserDto).subscribe({
      next: (response) => {
        alert('Usuário editado com sucesso!');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        console.error('Error editing user:', error);
        const errorMessage = error.error?.message || 'Erro ao editar usuário. Tente novamente.';
        alert(errorMessage);
      },
      complete: () => {
        console.log('Request to edit user completed.');
      }
    });
  }

}
