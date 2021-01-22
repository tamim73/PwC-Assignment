import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoadingBarService } from '@ngx-loading-bar/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(public loader: LoadingBarService, private router: Router) {}
  isAdmin() {
    return true;
  }
  isLoggedIn() {
    return true;
  }
  getName(): string {
    return 'Tamim';
  }
  login() {}
}
