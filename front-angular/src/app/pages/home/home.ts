import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-home',
  imports: [
    RouterLink,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatChipsModule,
    MatTooltipModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {

  users: User[] = [];
  allUsers: User[] = [];
  displayedColumns: string[] = ['status', 'name', 'lastName', 'department', 'actions'];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.GetUsers().subscribe({
      next: (response) => {
        const { data } = response;

        data.map(user => {
          user.createdAt = new Date(user.createdAt!).toLocaleDateString('pt-BR');
          user.updatedAt = new Date(user.updatedAt!).toLocaleDateString('pt-BR');
        });

        this.users = data;
        this.allUsers = data;
      },
      error: (error) => {
        console.error('Error:', error);
      },
      complete: () => {
        console.log('Request to get users completed.');
      }
    });
  }

  search(event: Event) {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase();

    this.users = this.allUsers.filter(user =>
      user.name.toLowerCase().includes(value)
    );
  }

}
