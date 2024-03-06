import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { EventModel } from '../models/EventModel';

@Injectable()

export class EventService {
  baseURL = 'http://localhost:5011/v1/events';
  constructor(private http: HttpClient) { }

  public getEvents() : Observable<EventModel[]>
  {
    return this.http
    .get<EventModel[]>(this.baseURL)
    .pipe(take(1));
  }

  public getEventsByTheme(theme: string) : Observable<EventModel[]>
  {
    return this.http
    .get<EventModel[]>(`${this.baseURL}/${theme}/theme`)
    .pipe(take(1));
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
}
