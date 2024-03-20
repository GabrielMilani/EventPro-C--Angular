import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SpeakerService } from '../../../services/speaker.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpeakerModel } from '../../../models/SpeakerModel';
import { debounceTime, map, tap } from 'rxjs';

@Component({
  selector: 'app-speaker-detail',
  templateUrl: './speaker-detail.component.html',
  styleUrls: ['./speaker-detail.component.scss']
})
export class SpeakerDetailComponent {

  public form!: FormGroup;
  public formState = '';
  public colorDescription = '';

  constructor(
    private fb: FormBuilder,
    public speakerService: SpeakerService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) {}

  ngOnInit() {
    this.validation();
    this.formVerify();
    this.loadSpeaker();
  }

  private validation(): void {
    this.form = this.fb.group({
      miniCV: [''],
    });
  }

  private loadSpeaker(): void {
    this.spinner.show();

    this.speakerService
      .getSpeaker()
      .subscribe(
        (speaker: SpeakerModel) => {
          this.form.patchValue(speaker);
        },
        (error: any) => {
          this.toastr.error('Load speaker error', 'Error')
        }
      )
  }

  public get f(): any {
    return this.form.controls;
  }

  private formVerify(): void {
    this.form.valueChanges
      .pipe(
        map(() => {
          this.formState = 'MiniCv changed!';
          this.colorDescription = 'text-warning';
        }),
        debounceTime(1000),
        tap(() => this.spinner.show())
      )
      .subscribe(() => {
        this.speakerService
          .put({...this.form.value })
          .subscribe(
            () => {
              this.formState = 'MiniCv change!';
              this.colorDescription = 'text-success';

              setTimeout(() => {
                this.formState = 'MiniCv loaded!';
                this.colorDescription = 'text-muted';
              }, 2000);
            },
            () => {
              this.toastr.error('Error changed speaker', 'Error');
            }
          ).add(() => this.spinner.hide())
      });
  }

}
