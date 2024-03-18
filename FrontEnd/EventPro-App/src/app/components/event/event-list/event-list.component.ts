import { PaginatedResult } from './../../../models/Pagination';
import { Component, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EventModel } from '../../../models/EventModel';
import { EventService } from '../../../services/event.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { Pagination } from '../../../models/Pagination';
import { Subject, debounceTime } from 'rxjs';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss'
})
export class EventListComponent {
  public modalRef: BsModalRef;
  public eventsModel: EventModel[] = [];
  public eventModelId: number = 0;
  public pagination = {} as Pagination;

  public widthImg: number = 75;
  public heightImg: number = 50;
  public marginImg: number = 2;
  public displayImg: boolean = true;

  public termSearshChanged: Subject<string> = new Subject<string>();

  constructor(private eventService: EventService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router){ }

  public ngOnInit(): void{
    this.pagination = {currentPage: 1, itemsPerPage: 3, totalItems: 1} as Pagination;
    this.LoadEvents();
  }

  public LoadEvents(): void{
    this.spinner.show();
    this.eventService
    .getEvents(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe(
      (paginatedResult : PaginatedResult<EventModel[]>) =>{
        this.eventsModel = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
      (error: any) =>{
        this.spinner.hide();
        this.toastr.error('Load events error', 'Error!');
      }).add(() => this.spinner.hide())
  }

  public filterEvents(evt: any): void{
    if (this.termSearshChanged.observers.length === 0){
      this.termSearshChanged.pipe(debounceTime(1000)).subscribe(
        filteredBy =>{
          this.spinner.show();
          this.eventService
          .getEvents(this.pagination.currentPage, this.pagination.itemsPerPage, filteredBy)
          .subscribe(
            (paginatedResult : PaginatedResult<EventModel[]>) =>{
              this.eventsModel = paginatedResult.result;
              this.pagination = paginatedResult.pagination;
            },
            (error: any) =>{
              this.spinner.hide();
              this.toastr.error('Load events error', 'Error!');
            }).add(() => this.spinner.hide())
        }
      )
    }
    this.termSearshChanged.next(evt.value);
  }

  public returnImage(imageURL: string): string{
    return (imageURL !== '') ? `${environment.apiURL}resources/images/${imageURL}` : 'assets/img/semImage.png';
  }

  public displayingImg() : void{
    this.displayImg = !this.displayImg;
  }
  public openModal(eventModel: any, template: TemplateRef<any>, eventModelId: number): void{
    eventModel.stopPropagation();
    this.eventModelId = eventModelId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public pageChanged(event): void{
    this.pagination.currentPage = event.page;
    this.LoadEvents();
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
