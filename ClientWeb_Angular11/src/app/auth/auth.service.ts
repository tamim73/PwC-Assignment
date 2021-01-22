import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import {
  IUserLoginRequest,
  ILoginResponse,
  IRegisterRequest,
} from './auth.DTO';
import { HttpClient } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { IBaseResponse } from '../core/Models';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertService } from '../core/services/alert.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // loading
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  private _isLoggedIn = false;
  private _isAdmin = false;
  private _loggedInAs = '';
  private _loggedInId = 0;

  private authEndPoint = environment.apiURL + '/auth';
  private loginUrl = this.authEndPoint + '/login';
  private registerUrl = this.authEndPoint + '/register';

  constructor(
    private http: HttpClient,
    private router: Router,
    private alertService: AlertService
  ) {
    const token = this.getToken();
    if (token) {
      this.validateToken(token);
    }
  }

  isLoggedIn() {
    return this._isLoggedIn;
  }

  loggedInAs(): string {
    return this._loggedInAs;
  }

  validateToken(token: string) {
    const helper = new JwtHelperService();
    const isExpired = helper.isTokenExpired(token);
    if (isExpired) {
      this.logout();
      return;
    }
    const decodedToken = helper.decodeToken(token);
    console.log(decodedToken);
    this._isAdmin =
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ] === 'Admin'
        ? true
        : false;
    this._loggedInAs =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'
      ];
    this._loggedInId = +decodedToken[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
    ];
    console.log(this._isAdmin);
    console.log(this._loggedInAs);
    console.log(this._loggedInId);
    this._isLoggedIn = true;
  }

  getUserId(): number {
    return this._loggedInId;
  }

  isAdmin() {
    return this._isAdmin;
  }

  getToken(): string | null {
    const token = localStorage.getItem('token');
    if (!token) {
      this.logout();
    }
    return token;
  }

  stopLoading() {
    this.loadingSubject.next(false);
  }

  startLoading() {
    this.loadingSubject.next(true);
  }

  logIn(request: IUserLoginRequest) {
    this.loadingSubject.next(true);
    this.http
      .post<ILoginResponse>(this.loginUrl, request)
      .pipe(finalize(() => this.stopLoading()))
      .subscribe((res) => {
        this.validateToken(res.accessToken);
        if (this._isLoggedIn) {
          localStorage.setItem('token', res.accessToken);
        }
        this.router.navigate(['']);
      });
  }

  register(request: IRegisterRequest) {
    this.loadingSubject.next(true);
    this.http
      .post<IBaseResponse>(this.registerUrl, request)
      .pipe(finalize(() => this.loadingSubject.next(false)))
      .subscribe((res) => {
        this.alertService.showSuccess(res.message);
        this.router.navigate(['/auth/login']);
      });
  }

  logout() {
    localStorage.removeItem('token');
    this._isLoggedIn = false;
    this._isAdmin = false;
    this._loggedInAs = '';
    this._loggedInId = 0;
    this.router.navigate(['']);
  }
}
