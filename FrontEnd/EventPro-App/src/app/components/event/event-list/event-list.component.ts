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
  public modalRef!: BsModalRef;
  public widthImg: number = 75;
  public heightImg: number = 50;
  public marginImg: number = 2;
  public displayImg: boolean = true;
  public events: EventModel[] = [];
  public filteredEvents: EventModel[] = [];
  public eventId: number = 0;

  private _listFilter: string = '';

  constructor(private eventService: EventService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router){ }

  public ngOnInit(): void
  {
    this.spinner.show();
    this.getEvents();
  }

  public getEvents(): void
  {
    const observ =
    {
      next:(EventResponse : EventModel[]) =>
      {
        this.events = EventResponse;
        this.filteredEvents = this.events;
      },
      error: (error: any) =>
      {
        this.spinner.hide();
        this.toastr.success('Load events error', 'Error!');
      },
      complete: () => this.spinner.hide()
     }
    this.eventService.getEvents().subscribe(observ)
  }

  public get listFilter(): string
  {
    return this._listFilter;
  }

  public set listFilter(value: string)
  {
    this._listFilter = value;
    this.filteredEvents = this.listFilter ? this.filterEvents(this.listFilter) : this.events;
  }

  public filterEvents(filterBy: string): EventModel[]
  {
    filterBy = filterBy.toLocaleLowerCase();
      return this.events.filter(
        (event: {theme: string; local: string;}) => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
                                                    event.local.toLocaleLowerCase().indexOf(filterBy)!== -1)
  }

  public displayingImg() : void
  {
    this.displayImg = !this.displayImg;
  }
  public openModal(event: any, template: TemplateRef<any>, eventId: number): void
  {
    event.stopPropagation();
    this.eventId = eventId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void
  {
    this.modalRef.hide();
    this.spinner.show();

    this.eventService.deleteEvent(this.eventId).subscribe(
      (result: any) =>
      {
        console.log(result);
        this.toastr.success('Event deleted success', 'Deleted!');
        this.spinner.hide();
        this.getEvents();
      },
      (error: any) =>
        {
          console.error(error);
          this.toastr.success('Delete events error', 'Error!');
          this.spinner.hide();
        },
      () => this.spinner.hide()
     );
    this.toastr.success('Event deleted success!', 'Deleted');
  }

  public decline(): void
  {
    this.modalRef.hide();
  }
  public detailingEvent(id: number): void
  {
    this.router.navigate([`events/detail/${id}`]);
  }

}
