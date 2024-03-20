import { SpeakerModel } from './../../../models/SpeakerModel';
import { Component } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { SpeakerService } from '../../../services/speaker.service';
import { Subject, debounceTime } from 'rxjs';
import { PaginatedResult, Pagination } from '../../../models/Pagination';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-speaker-list',
  templateUrl: './speaker-list.component.html',
  styleUrls: ['./speaker-list.component.scss']
})
export class SpeakerListComponent {

  public speakersModel: SpeakerModel[] = [];
  public eventModelId: number = 0;
  public pagination = {} as Pagination;
  public termSearshChanged: Subject<string> = new Subject<string>();

  constructor(private speakerService: SpeakerService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router) { }

  public filterSpeakers(evt: any): void{
    if (this.termSearshChanged.observers.length === 0){
      this.termSearshChanged.pipe(debounceTime(1000)).subscribe(
        filteredBy =>{
          this.spinner.show();
          this.speakerService
          .getSpeakers(this.pagination.currentPage, this.pagination.itemsPerPage, filteredBy)
          .subscribe(
            (paginatedResult : PaginatedResult<SpeakerModel[]>) =>{
              this.speakersModel = paginatedResult.result;
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

  public ngOnInit() {
    this.pagination = {currentPage: 1, itemsPerPage: 3, totalItems: 1} as Pagination;
    this.loadSpeakers();
  }

  public getImageUrl(imageName: string): string{
    if(imageName)
      return environment.apiURL+ `resources/profile/` + imageName;
    else
      return './assets/img/img-profile-empty.jpg';
  }

  public loadSpeakers(): void{
    this.spinner.show();
    this.speakerService
    .getSpeakers(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe(
      (paginatedResult : PaginatedResult<SpeakerModel[]>) =>{
        this.speakersModel = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
      (error: any) =>{
        this.spinner.hide();
        this.toastr.error('Load speaker error', 'Error!');
      }).add(() => this.spinner.hide())
  }

}
