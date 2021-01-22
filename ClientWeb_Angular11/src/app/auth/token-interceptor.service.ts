import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService {

  constructor(private authService: AuthService) { }
  intercept(req: any, next: any) {
    const tokenizedReq = req.clone(
      {
        headers: req.headers.set('Authorization', 'Bearer ' + this.authService.getToken())
      }
    );
    return next.handle(tokenizedReq);
  }
}
