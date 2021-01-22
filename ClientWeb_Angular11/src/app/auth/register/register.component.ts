import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AssignmentFormField, matchValidator } from 'src/app/core/FormHelper';
import { IRegisterRequest } from '../auth.DTO';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerFG: FormGroup;
  fields: AssignmentFormField[];

  constructor(private fb: FormBuilder, public authService: AuthService) {
    this.registerFG = this.fb.group({
      name: ['', [Validators.required]],
      username: ['', [Validators.required]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(18),
        ],
      ],
      confirmPassword: ['', [matchValidator('password')]],
      isAdmin: [false]
    });

    this.fields = [
      {
        name: 'name',
        errors: { required: 'Name is required' },
        label: 'Name',
        type: 'text',
        placeholder: 'Enter your name',
      },
      {
        name: 'username',
        errors: { required: 'Username is required' },
        label: 'Username',
        type: 'text',
        placeholder: 'Enter username',
      },
      {
        name: 'password',
        errors: {
          required: 'Password is required',
          minlength: 'minimum length is 6',
          maxlength: 'maximum length is 18',
        },
        label: 'Password',
        type: 'password',
        placeholder: 'Enter password',
      },
      {
        name: 'confirmPassword',
        errors: { match: 'Password does not match' },
        label: 'Confirm Password',
        type: 'password',
        placeholder: 'Re-enter password',
      },
    ];
  }

  ngOnInit() {}

  getErrorMessage(ctrlName: string): string | undefined {
    if (this.registerFG.controls[ctrlName].invalid) {
      const errors = this.registerFG.controls[ctrlName].errors || {};
      const key = Object.keys(errors)[0];
      return this.fields.find(field => field.name === ctrlName)?.errors[key];
    }
    return undefined;
  }

  onRegisterClick(): void {
    if (this.registerFG.valid) {
      const {name, username, password, isAdmin} = this.registerFG.value;
      const req: IRegisterRequest = {
        name,
        username,
        password,
        role: isAdmin ? 'Admin' : 'Writer',
      };
      this.authService.register(req);
    }
  }
}
