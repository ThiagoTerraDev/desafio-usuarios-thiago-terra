import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-delete',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './delete.html',
  styleUrl: './delete.css'
})
export class Delete {
  isDeleting = false;
  errorMessage = '';

  constructor(
    public dialogRef: MatDialogRef<Delete>,
    @Inject(MAT_DIALOG_DATA) public user: User,
    private userService: UserService
  ) {}

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.isDeleting = true;
    this.errorMessage = '';

    this.userService.DeleteUser(this.user.id!).subscribe({
      next: (response) => {
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.isDeleting = false;
        this.errorMessage = 'Erro ao excluir usuário. Tente novamente.';
        console.error('Error:', error);
      }
    });
  }
}
