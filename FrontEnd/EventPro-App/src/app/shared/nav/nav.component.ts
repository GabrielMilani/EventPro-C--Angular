import { AccountService } from './../../services/account.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {
  isCollapsed = true;

  constructor(public accountService: AccountService,
              private router: Router){ }

  ngOnInit(): void { }

  logout(): void{
    this.accountService.logout();
    this.router.navigateByUrl('/user/login');
  }

  public showMenu(): boolean{
    return this.router.url !== '/user/login';
  }
}
