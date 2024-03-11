import { Component, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { LotModel } from '../../../models/LotModel';
import { EventModel } from './../../../models/EventModel';
import { LotService } from './../../../services/lot.service';
import { EventService } from '../../../services/event.service';


@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss'
})
export class EventDetailComponent {
  modalRef: BsModalRef;
  eventModelId: number;
  eventModel = {}  as EventModel;
  form: FormGroup;
  stateSave = 'post';
  currentLot = {id: 0, name: '', indice: 0}

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
              private modalService: BsModalService,
              private router: Router,
              private lotService: LotService){
    this.localeService.use('pt-br')
  }

  public loadEvent(): void{
    this.eventModelId = +this.activatedRouter.snapshot.paramMap.get('id');

    if(this.eventModelId !== null && this.eventModelId !== 0){
      this.spinner.show();

      this.stateSave = 'put';

      this.eventService
      .getEventById(this.eventModelId)
      .subscribe(
      (eventModel: EventModel) =>{
          this.eventModel = {...eventModel};
          this.form.patchValue(this.eventModel);
          this.loadLots();
        },
      (error: any) =>{
          console.error(error);
          this.toastr.error('Failed event insert.', 'Error!');
        }
      )
      .add(() => this.spinner.hide());
    }
  }

  public loadLots(): void {
    this.lotService
    .getLotsByEventId(this.eventModelId)
    .subscribe(
      (lotsRetorno: LotModel[]) => {
        lotsRetorno.forEach(lotRetorno => {
          this.lots.push(this.createLot(lotRetorno));
        })
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Failed load lots.', 'Error!');
      }
    ).add(() => this.spinner.hide());
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

  public changeValueDate(value: Date, indice: number, campo: string): void{
    this.lots.value[indice][campo] = value;
  }

  public returnTitleLot(name: string): string{
    return name === null || name === '' ? 'Name Lot' : name;
  }

  public resetForm(): void{
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any{
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public saveEvents(): void{
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
  public saveLots(): void{
    if(this.form.controls['lots'].valid){
      this.spinner.show;
      this.lotService.saveLot(this.eventModelId, this.form.value.lots)
      .subscribe(
        ()=>{
          this.toastr.success('Save lots success', 'Success!');
        },
        (error: any)=>{
          console.error(error);
          this.toastr.error('Save lots error','Error!')
        },
      ).add(() =>{this.spinner.hide()})
    }
  }
  public removeLot(template: TemplateRef<any>, indice: number): void{

    this.currentLot.id = this.lots.get(indice + '.id').value;
    this.currentLot.name = this.lots.get(indice + '.name').value;
    this.currentLot.indice = indice;

    this.modalRef = this.modalService.show(template, {class: 'modal-sm' });
  }

  public confirmDeleteLot():void{
    this.modalRef.hide();
    this.spinner.show();

    this.lotService
    .deleteLotByIds(this.eventModelId, this.currentLot.id)
    .subscribe(
      ()=>{
        this.toastr.success('Deleted lot success', 'Success!');
        this.lots.removeAt(this.currentLot.indice);
      },
      (error: any)=>{
        console.error(error);
        this.toastr.error('Deleted lots error','Error!')
      },
    ).add(() =>{this.spinner.hide()})
  }

  public declineDeleteLot(): void{
    this.modalRef.hide();
  }

}
