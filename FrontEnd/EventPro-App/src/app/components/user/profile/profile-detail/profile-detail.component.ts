import { Component, EventEmitter, Output } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserUpdate } from '../../../../models/identity/UserUpdate';
import { ValidatorField } from '../../../../helpers/ValidatorField';
import { SpeakerService } from '../../../../services/speaker.service';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.scss']
})
export class ProfileDetailComponent {
  @Output() changeFormValue = new EventEmitter();

  userUpdate = {} as UserUpdate;
  form!: FormGroup;

  constructor(private fb: FormBuilder,
              public accountService: AccountService,
              public speakerService: SpeakerService,
              private router: Router,
              private toaster: ToastrService,
              private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.validation();
    this.loadUser();
    this.formVerify();
  }

  private formVerify(): void{
    this.form.valueChanges
    .subscribe(
      () => { this.changeFormValue.emit({...this.form.value})}
    )
  }

  private loadUser(): void{
    this.spinner.show();
    this.accountService
    .getUser()
    .subscribe(
      (userReturn: UserUpdate) => {
        this.userUpdate = userReturn;
        this.form.patchValue(this.userUpdate);
        this.toaster.success('User loaded success.', 'Success!');
      },
      (error: any) => {
        console.error(error);
        this.toaster.error('User loaded error.', 'Error!');
        //this.router.navigate(['/dashboard']);
      }
    ).add(() => this.spinner.hide())
  }

  private validation(): void{
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'passwordConfirmation')
    };
    this.form = this.fb.group({
      userName: [''],
      imageUrl: [''],
      title: ['NaoInformado', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      description: ['', Validators.required],
      function: ['NaoInformado', Validators.required],
      password: ['', [Validators.minLength(4), Validators.nullValidator]],
      passwordConfirmation: ['', Validators.nullValidator],
    },formOptions);
  }
  public get f(): any{
    return this.form.controls;
  }

  public onSubmit(): void{
    this.updatedUser();
  }

  public updatedUser(): void{
    this.userUpdate = {...this.form.value }
    this.spinner.show();
    if (this.f.function.value == 'Palestrante'){
      this.speakerService.post().subscribe(
        () => {
          this.toaster.success('Function speaker activated!', "Success!");
        },
        (error: any) => {
          console.error(error);
          this.toaster.error('Function speaker not activated!', "Error!");
        }
      )
    }

    this.accountService
    .updateUser(this.userUpdate)
    .subscribe(
      () => {
        this.toaster.success('User updated success', 'Success!');
      },
      (error: any) => {
        console.error(error);
        this.toaster.error('User updated error', 'Error!');
      },
    ).add(() => this.spinner.hide())
  }

  public resetForm(): void{
    this.form.reset();
  }
}
