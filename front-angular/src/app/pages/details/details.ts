import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-details',
  imports: [
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatChipsModule,
    MatIconModule,
    MatDividerModule,
    MatTooltipModule
  ],
  templateUrl: './details.html',
  styleUrl: './details.css'
})
export class Details implements OnInit {

  userData?: User;
  userId!: number;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.userId) {
      this.userService.GetUserById(this.userId).subscribe({
        next: (response) => {
          const { data } = response;

          data.createdAt = this.formatDateTime(data.createdAt!);
          data.updatedAt = this.formatDateTime(data.updatedAt!);

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
        this.router.navigate(['/dashboard']);
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

  private formatDateTime(dateString: string): string {
    const localDate = new Date(dateString + 'Z');

    const day = String(localDate.getDate()).padStart(2, '0');
    const month = String(localDate.getMonth() + 1).padStart(2, '0');
    const year = localDate.getFullYear();

    return `${day}/${month}/${year}`;
  }

  isCurrentUser(): boolean {
    const currentUserId = this.authService.currentUserValue?.userId;
    return currentUserId === this.userId;
  }

}
