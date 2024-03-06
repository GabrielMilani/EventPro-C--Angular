import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

import { EventModel } from './../../../models/EventModel';
import { EventService } from '../../../services/event.service';


@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss'
})
export class EventDetailComponent {
  eventModel = {}  as EventModel;
  form!: FormGroup;

  public get f(): any
  {
    return this.form.controls;
  }

  public get bsConfig(): any
  {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private eventService: EventService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br')
  }

  public loadEvent(): void
  {
    const eventIdParam = this.router.snapshot.paramMap.get('id');

    if(eventIdParam !== null){
      this.spinner.show();
      this.eventService.getEventById(+eventIdParam).subscribe(
        (eventModel: EventModel) =>
          {
            this.eventModel = {...eventModel}
            this.form.patchValue(this.eventModel);
          },
        (error: any) =>
          {
            this.spinner.hide();
            this.toastr.error('Load event error.', 'Error!')
            console.error(error);
          },
        () => this.spinner.hide(),
      );
    }
  }

  ngOnInit(): void
  {
    this.loadEvent();
    this.validation();
  }

  public validation(): void
  {
    this.form = this.fb.group({
      theme: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      local: ['', Validators.required],
      eventDate: ['', Validators.required],
      quantityPeople: ['', [Validators.required, Validators.max(120000)]],
      email: ['', [Validators.required, Validators.email]],
      imageUrl: ['', Validators.required],
    });
  }

  public resetForm(): void
  {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any
  {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public saveChanges(): void
  {
    this.spinner.show();
    if(this.form.valid)
    {
      this.eventModel = {...this.form.value};
      this.eventService.postEvent(this.eventModel).subscribe(
        () => this.toastr.success('Event saved success.', 'Success!'),
        (error: any) =>
          {
            console.error(error)
            this.spinner.hide();
            this.toastr.error('Failed event insert.', 'Error!')
          },
        () => {this.spinner.hide()},
      );
    }
  }
}
