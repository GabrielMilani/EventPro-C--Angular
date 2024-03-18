import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, take } from 'rxjs';
import { EventModel } from '../models/EventModel';
import { environment } from '../../environments/environment';
import { PaginatedResult } from '../models/Pagination';

@Injectable()

export class EventService {
  baseURL = environment.apiURL+'v1/events';

  constructor(private http: HttpClient) { }

  public getEvents(page?: number, itemsPerPage?: number, term?: string) : Observable<PaginatedResult<EventModel[]>>
  {
    const paginatedResult : PaginatedResult<EventModel[]> = new PaginatedResult<EventModel[]>();
    let params = new HttpParams;

    if (page != null && itemsPerPage != null){
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    if (term != null && term != ''){
      params = params.append('term', term)
    }

    return this.http
    .get<EventModel[]>(this.baseURL, {observe: 'response', params})
    .pipe(take(1), map((response) =>{
      paginatedResult.result = response.body;
      if(response.headers.has('Pagination')){
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    }));
  }

  public getEventById(id: number) : Observable<EventModel>
  {
    return this.http
    .get<EventModel>(`${this.baseURL}/${id}`)
    .pipe(take(1));
  }

  public post(event: EventModel) : Observable<EventModel>
  {
    return this.http
    .post<EventModel>(this.baseURL, event)
    .pipe(take(1));
  }

  public put(event: EventModel) : Observable<EventModel>
  {
    return this.http
    .put<EventModel>(`${this.baseURL}/${event.id}`, event)
    .pipe(take(1));
  }

  public deleteEvent(id: number): Observable<any>
  {
    return this.http
    .delete(`${this.baseURL}/${id}`)
    .pipe(take(1));
  }
  public postUpload(eventId: number, file: File): Observable<EventModel>{
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
    .post<EventModel>(`${this.baseURL}/upload-image/${eventId}`, formData)
    .pipe(take(1));
  }
}
