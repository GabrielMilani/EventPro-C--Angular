import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {
  isCollapsed = true;

  constructor(private router: Router){}
  ngOnInit(): void {  }

  public showMenu(): boolean{
    return this.router.url !== '/user/login';
  }
}
