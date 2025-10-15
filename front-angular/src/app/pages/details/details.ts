import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-details',
  imports: [RouterLink],
  templateUrl: './details.html',
  styleUrl: './details.css'
})
export class Details implements OnInit {

  userData?: User;
  userId!: number;

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.userId) {
      this.userService.GetUserById(this.userId).subscribe({
        next: (response) => {
          const { data } = response;

          data.createdAt = new Date(data.createdAt!).toLocaleDateString('pt-BR')
          data.updatedAt = new Date(data.updatedAt!).toLocaleDateString('pt-BR')

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

  deactivateUser(): void {
    console.log('deactivateUser chamado. userId:', this.userId);

    if (!this.userId) {
      alert('ID do usuário inválido.');
      return;
    }

    console.log('Chamando API para desativar usuário...');
    this.userService.DeactivateUser(this.userId).subscribe({
      next: (response) => {
        console.log('Resposta do backend:', response);
        alert('Usuário desativado com sucesso.');
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Error deactivating user:', error);
        alert('Erro ao desativar usuário.');
      },
      complete: () => {
        console.log('Request to deactivate user completed.');
      }
    });
  }

}
