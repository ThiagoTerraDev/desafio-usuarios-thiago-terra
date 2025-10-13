import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {

  users: User[] = [];
  allUsers: User[] = [];

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

        console.log('Users fetched successfully:', this.users);
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
