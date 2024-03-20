import { Component } from '@angular/core';
import { UserUpdate } from '../../../models/identity/UserUpdate';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../../services/account.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  public user = {} as UserUpdate;
  public file: File;
  public imageURL = '';

  public get isSpeaker(): boolean{
    return this.user.function === 'Palestrante';
 }

  constructor(public accountService: AccountService,
              private spinner: NgxSpinnerService,
              private toaster: ToastrService) { }

  ngOnInit(): void {

  }

  public onFileChange(ev: any): void{
    const reader = new FileReader();

    reader.onload = (event: any) => this.imageURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);
    this.uploadImage();
  }

  private uploadImage(): void{
    this.spinner.show();
    this.accountService
    .postUpload(this.file).subscribe(
      () => {this.toaster.success('Image changed success.', 'Success!')},
      (error: any) => {
        console.error(error);
        this.toaster.error('Image changed error.', 'Error!')
      }
    ).add(() => this.spinner.hide())
  }

  public setFormValue(user: UserUpdate): void{
    this.user = user;
    if(this.user.imageUrl)
      this.imageURL = environment.apiURL+ `resources/profile/` + this.user.imageUrl;
    else
      this.imageURL = './assets/img/img-profile-empty.jpg';
  }

}
