import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { IUserLoginRequest } from '../auth.DTO';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginFG: FormGroup;
  hidePass = true;

  constructor(public authService: AuthService, private fb: FormBuilder) {
    this.loginFG = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit() {}

  onLogInClick() {
    if (this.loginFG.valid) {
      this.authService.logIn(this.loginFG.value);
    }
  }
}
