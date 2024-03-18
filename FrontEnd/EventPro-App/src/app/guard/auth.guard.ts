import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router,
              private toaster: ToastrService) {}

  canActivate(): boolean  {
    if(localStorage.getItem('user') !== null)
      return true;
    this.toaster.info('User not authenticate!')
    this.router.navigate(['/user/login']);
    return false;
  }

}