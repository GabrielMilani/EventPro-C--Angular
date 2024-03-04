import { Component, Input} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shared-title',
  templateUrl: './shared-title.component.html',
  styleUrl: './shared-title.component.scss'
})
export class SharedTitleComponent
{
  @Input() baseTitle!: string;
  @Input() subTitle!: string;
  @Input() iconClass = 'fa fa-user';
  @Input() btnList = false;

  constructor(private router: Router){}

  ngOnInit(): void {}

  listing(): void
  {
    this.router.navigate([`/${this.baseTitle.toLocaleLowerCase()}/list`]);
  }
}
