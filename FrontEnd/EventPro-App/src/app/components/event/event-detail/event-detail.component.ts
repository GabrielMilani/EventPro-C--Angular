import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef } from 'ngx-bootstrap/modal';

import { EventModel } from './../../../models/EventModel';
import { EventService } from '../../../services/event.service';
import { LotModel } from '../../../models/LotModel';


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
  stateSave = 'post';

  public get modeEdit(): boolean{
    return this.stateSave === 'put';
  }

  public get lots(): FormArray{
    return this.form.get('lots') as FormArray
  }

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
              private toastr: ToastrService,
              private router: Router){
    this.localeService.use('pt-br')
  }

  public loadEvent(): void{
    this.eventId = +this.activatedRouter.snapshot.paramMap.get('id');

    if(this.eventId !== null && this.eventId !== 0){
      this.spinner.show();

      this.stateSave = 'put';

      this.eventService
      .getEventById(this.eventId)
      .subscribe(
      (eventModel: EventModel) =>{
          this.eventModel = {...eventModel};
          this.form.patchValue(this.eventModel);
        },
      (error: any) =>{
          console.error(error);
          this.toastr.error('Failed event insert.', 'Error!');
        }
      )
      .add(() => this.spinner.hide());
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
      lots: this.fb.array([])
    });
  }

  public addLot(): void{
    this.lots.push(this.createLot({id: 0} as LotModel));
  }

  public createLot(lot: LotModel): FormGroup{
    return this.fb.group({
      id: [lot.id],
      name: [lot.name, Validators.required],
      quantity: [lot.quantity, Validators.required],
      price: [lot.price, Validators.required],
      initialDate: [lot.initialDate],
      finalDate: [lot.finalDate],
    })
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
      this.eventModel = (this.stateSave === 'post')
                    ? {...this.form.value}
                    : {id: this.eventModel.id, ...this.form.value};

      this.eventService[this.stateSave](this.eventModel).subscribe(
        (eventReturn: EventModel) =>{
          this.toastr.success('Event saved success.', 'Success!');
          this.router.navigate([`events/detail/${eventReturn.id}`]);
        },
        (error: any) =>{
          console.error(error);
          this.toastr.error('Failed event insert.', 'Error!');
        }).add(() =>{this.spinner.hide()});
    }
  }
}
