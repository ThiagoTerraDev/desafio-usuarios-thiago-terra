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
import { MatDialog } from '@angular/material/dialog';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user';
import { Delete } from '../../components/delete/delete';

@Component({
  selector: 'app-dashboard',
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
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {

  users: User[] = [];
  allUsers: User[] = [];
  displayedColumns: string[] = ['status', 'name', 'lastName', 'email', 'department', 'actions'];

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  search(event: Event) {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase();

    this.users = this.allUsers.filter(user =>
      user.name.toLowerCase().includes(value)
    );
  }

  openDeleteDialog(user: User): void {
    const dialogRef = this.dialog.open(Delete, {
      width: '550px',
      data: user,
      disableClose: false,
      autoFocus: false,
      panelClass: 'delete-dialog-container',
      backdropClass: 'delete-dialog-backdrop'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.loadUsers();
      }
    });
  }

  private loadUsers(): void {
    this.userService.GetUsers().subscribe({
      next: (response) => {
        this.users = response.data;
        this.allUsers = response.data;
      },
      error: (error) => {
        console.error('Error:', error);
      }
    });
  }

  logout(): void {
    this.authService.logout();
  }
}
