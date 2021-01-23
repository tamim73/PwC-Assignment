import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoadingBarService } from '@ngx-loading-bar/core';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(
    private router: Router,
    private authService: AuthService,
    public loader: LoadingBarService
  ) {}

  isAdmin() {
    return this.authService.isAdmin();
  }
  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
  getName(): string {
    return this.authService.loggedInAs();
  }
  getRole(): string {
    return this.authService.getUserRole();
  }
  login() {
    if (this.isLoggedIn()) {
      this.authService.logout();
    } else {
      this.router.navigate(['auth/login']);
    }
  }
}
