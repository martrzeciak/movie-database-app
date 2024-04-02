import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  
  constructor(
    private formBuilder: FormBuilder, private accountService: AccountService,
    private router: Router) {}

  ngOnInit(): void {
    this.initalizeForm();
  }

  initalizeForm() {
    this.registerForm = this.formBuilder.group({
      gender: ['male'],
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', 
        [Validators.required, 
         Validators.minLength(4), 
         Validators.maxLength(20)]],
      confirmPassword: ['', 
        [Validators.required,
         this.matchValues('password')]]
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }

  register() {
    const values = {...this.registerForm.value};
    this.accountService.register(values).subscribe({
      next: _ =>  {
        this.router.navigateByUrl('/')
      },
      error: error => {
        this.validationErrors = error;
      }
    })
  }
}
