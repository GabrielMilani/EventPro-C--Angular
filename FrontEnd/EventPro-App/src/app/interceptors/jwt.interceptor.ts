import { AccountService } from './../services/account.service';
import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, take } from "rxjs";
import { User } from "../models/identity/User";

@Injectable()
export class JwtInterceptor implements HttpInterceptor{

  constructor(private accountService: AccountService){ }

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User;

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      currentUser = user

      if(currentUser){
        req = req.clone({
          setHeaders: {
            Authorization: `Bearer ${currentUser.token}`
          }
        });
      }
    });
    return next.handle(req)
  }
}
