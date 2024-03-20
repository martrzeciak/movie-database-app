import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-user-list',
  templateUrl: './admin-user-list.component.html',
  styleUrls: ['./admin-user-list.component.css']
})
export class AdminUserListComponent implements OnInit{
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router) {}

    ngOnInit(): void {
      this.initalizeForm();
    }

    initalizeForm() {
      this.registerForm = this.formBuilder.group({
        gender: ['male'],
        username: ['', Validators.required],
        password: ['', 
          [Validators.required, 
          Validators.minLength(4), 
          Validators.maxLength(20)]]
      });
    }

    addMovie() {

    }
}
