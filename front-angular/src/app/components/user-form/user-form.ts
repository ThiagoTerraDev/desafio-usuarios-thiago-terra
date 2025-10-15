import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { User } from '../../models/user';

@Component({
  selector: 'app-user-form',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './user-form.html',
  styleUrl: './user-form.css'
})
export class UserForm implements OnInit {

  @Output() onSubmit = new EventEmitter<User>();
  @Input() btnTitle!: string;
  @Input() btnAction!: string;
  @Input() userData: User | null = null;

  userForm!: FormGroup;

  ngOnInit(): void {
    this.userForm = new FormGroup({
      id: new FormControl(this.userData ? this.userData?.id : 0),
      name: new FormControl(this.userData ? this.userData?.name : '', [Validators.required]),
      lastName: new FormControl(this.userData ? this.userData?.lastName : '', [Validators.required]),
      department: new FormControl(this.userData ? this.userData?.department : '', [Validators.required]),
      shift: new FormControl(this.userData ? this.userData?.shift : '', [Validators.required]),
      active: new FormControl(this.userData ? this.userData?.active : true),
      createdAt: new FormControl(new Date()),
      updatedAt: new FormControl(new Date())
    });
  }

  submit(): void {
    this.onSubmit.emit(this.userForm.value);
  }

}
