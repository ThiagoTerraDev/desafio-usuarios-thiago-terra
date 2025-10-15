import { Component, OnInit } from '@angular/core';
import { UserForm } from "../../components/user-form/user-form";
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  imports: [UserForm],
  templateUrl: './edit.html',
  styleUrl: './edit.css'
})
export class Edit implements OnInit{

  constructor(private userService : UserService, private route: ActivatedRoute, private router: Router) { }

  btnTitle: string = 'Editar Usuário';
  btnAction: string = 'Editar';
  userData! : User;

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
      this.router.navigate(['/']);
    }
  }

  editUser(user: User): void {
    this.userService.EditUser(user).subscribe({
      next: (response) => {
        alert('Usuário editado com sucesso!');
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Error editing user:', error);
        alert('Erro ao editar usuário.');
      },
      complete: () => {
        console.log('Request to edit user completed.');
      }
    });
  }

}
