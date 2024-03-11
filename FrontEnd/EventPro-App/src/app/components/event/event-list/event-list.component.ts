import { Component, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EventModel } from '../../../models/EventModel';
import { EventService } from '../../../services/event.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss'
})
export class EventListComponent {
  public modalRef: BsModalRef;
  public eventsModel: EventModel[] = [];
  public filteredEvents: EventModel[] = [];
  public eventModelId: number = 0;

  public widthImg: number = 75;
  public heightImg: number = 50;
  public marginImg: number = 2;
  public displayImg: boolean = true;

  private _listFilter: string = '';

  constructor(private eventService: EventService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router){ }

  public ngOnInit(): void{
    this.spinner.show();
    this.LoadEvents();
  }

  public LoadEvents(): void{
    this.spinner.show();
    this.eventService.getEvents().subscribe(
      (EventResponse : EventModel[]) =>{
        this.eventsModel = EventResponse;
        this.filteredEvents = this.eventsModel;
      },
      (error: any) =>{
        this.spinner.hide();
        this.toastr.error('Load events error', 'Error!');
      }).add(() => this.spinner.hide())
  }

  public get listFilter(): string{
    return this._listFilter;
  }

  public set listFilter(value: string){
    this._listFilter = value;
    this.filteredEvents = this.listFilter ? this.filterEvents(this.listFilter) : this.eventsModel;
  }

  public filterEvents(filterBy: string): EventModel[]{
    filterBy = filterBy.toLocaleLowerCase();
      return this.eventsModel.filter(
        (eventModel: {theme: string; local: string;}) => eventModel.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
                                                         eventModel.local.toLocaleLowerCase().indexOf(filterBy)!== -1)
  }

  public displayingImg() : void{
    this.displayImg = !this.displayImg;
  }
  public openModal(eventModel: any, template: TemplateRef<any>, eventModelId: number): void{
    eventModel.stopPropagation();
    this.eventModelId = eventModelId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void{
    this.modalRef.hide();
    this.spinner.show();

    this.eventService.deleteEvent(this.eventModelId).subscribe(
      (result: any) =>{
        this.toastr.success('Event deleted success', 'Deleted!');
        this.LoadEvents();
      },
      (error: any) =>{
        console.error(error);
        this.toastr.error('Delete events error', 'Error!');
      }).add(() => this.spinner.hide());
  }

  public decline(): void{
    this.modalRef.hide();
  }
  public detailingEvent(id: number): void{
    this.router.navigate([`events/detail/${id}`]);
  }

}
