import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-user-form',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule,
    CommonModule,
    MatIconModule
  ],
  templateUrl: './user-form.html',
  styleUrl: './user-form.css'
})
export class UserForm implements OnInit {

  @Output() onSubmit = new EventEmitter<User>();
  @Input() formTitle!: string;
  @Input() btnAction!: string;
  @Input() userData: User | null = null;

  userForm!: FormGroup;
  isEditMode = false;
  hidePassword = true;

  ngOnInit(): void {
    this.isEditMode = !!this.userData;

    this.userForm = new FormGroup({
      id: new FormControl(this.userData ? this.userData?.id : 0),
      name: new FormControl(this.userData ? this.userData?.name : '', [Validators.required]),
      lastName: new FormControl(this.userData ? this.userData?.lastName : '', [Validators.required]),
      email: new FormControl(this.userData ? this.userData?.email : '', [Validators.required, Validators.email]),
      department: new FormControl(this.userData ? this.userData?.department : '', [Validators.required]),
      shift: new FormControl(this.userData ? this.userData?.shift : '', [Validators.required]),
      active: new FormControl(this.userData ? this.userData?.active : true),
      createdAt: new FormControl(new Date()),
      updatedAt: new FormControl(new Date())
    });

    // Adiciona senha provisória apenas na criação (não na edição)
    if (!this.isEditMode) {
      this.userForm.addControl(
        'password',
        new FormControl('', [Validators.required, Validators.minLength(6)])
      );
    }
  }

  submit(): void {
    this.onSubmit.emit(this.userForm.value);
  }

}
