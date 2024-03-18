import { AccountService } from './../../../services/account.service';
import { Component } from '@angular/core';
import { UserLogin } from '../../../models/identity/UserLogin';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  model = {} as UserLogin;

  constructor(private accountService: AccountService,
              private router: Router,
              private toaster: ToastrService){}

  ngOnInit(): void {}

  public login(): void{
    this.accountService.login(this.model).subscribe(
      () => {
        this.router.navigateByUrl('/home');
      },
      (error: any) => {
        if(error.status == 401)
          this.toaster.error('User or password invalid.', 'Error!')
        else
          console.error(error);
      }
    );
  }
}
