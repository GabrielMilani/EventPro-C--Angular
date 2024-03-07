import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef } from 'ngx-bootstrap/modal';

import { EventModel } from './../../../models/EventModel';
import { EventService } from '../../../services/event.service';


@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss'
})
export class EventDetailComponent {
  modalRef: BsModalRef;
  eventId: number;
  eventModel = {}  as EventModel;
  form!: FormGroup;
  modeSave = 'post';

  public get f(): any{
    return this.form.controls;
  }

  public get bsConfig(): any{
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private activatedRouter: ActivatedRoute,
              private eventService: EventService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService){
    this.localeService.use('pt-br')
  }

  public loadEvent(): void{
    this.eventId = +this.activatedRouter.snapshot.paramMap.get('id');

    if(this.eventId !== null){
      this.spinner.show();

      this.modeSave = 'put';

      this.eventService.getEventById(this.eventId).subscribe(
      (eventModel: EventModel) =>{
          this.eventModel = {...eventModel}
          this.form.patchValue(this.eventModel);
        },
      (error: any) =>{
          this.toastr.error('Load event error.', 'Error!')
          console.error(error);
        }).add(() => this.spinner.hide());
    }
  }

  ngOnInit(): void{
    this.loadEvent();
    this.validation();
  }

  public validation(): void{
    this.form = this.fb.group({
      theme: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      local: ['', Validators.required],
      telephone: ['', Validators.required],
      eventDate: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageUrl: ['', Validators.required],
      quantityPeople: ['', [Validators.required, Validators.max(120000)]],
    });
  }

  public resetForm(): void{
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any{
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public saveChanges(): void{
    this.spinner.show();
    if(this.form.valid){
      this.eventModel = (this.modeSave === 'post')
                    ? {...this.form.value}
                    : {id: this.eventModel.id, ...this.form.value};

      this.eventService[this.modeSave](this.eventModel).subscribe(
        () =>{ this.toastr.success('Event saved success.', 'Success!')},
        (error: any) =>{
          console.error(error);
          this.toastr.error('Failed event insert.', 'Error!');
        }).add(() =>{this.spinner.hide()});
    }
  }
}
