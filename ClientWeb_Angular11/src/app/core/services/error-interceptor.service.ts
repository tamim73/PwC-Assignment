import { Injectable } from '@angular/core';
import { AlertService } from './alert.service';
import { AuthService } from 'src/app/auth/auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(
    private alertService: AlertService,
    private authService: AuthService,
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(1),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            // client-side error
            errorMessage = `Error: ${error.error.message}`;
          } else {
            // server-side error

            switch (error.status) {

              // bad request
              case 400:
                this.alertService.showWarning(error.error.message || 'Bad request');
                break;

              // Unauthorized
              case 401:
                this.alertService.showWarning('Login expired, please login again.');
                this.authService.logout();
                break;

              // not found
              case 403:
                this.alertService.showWarning(error.error.message || 'Access denied');
                break;

              // not found
              case 404:
                this.alertService.showWarning(error.error.message || 'Not found');
                break;

              // unhandled
              default:
                this.alertService.showError('Oops!  something went wrong in server');
                break;
            }

            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          }
          return throwError(errorMessage);
        })
      );
  }

}
