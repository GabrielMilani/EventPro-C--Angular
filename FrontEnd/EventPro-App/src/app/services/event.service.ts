import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EventModel } from '../models/EventModel';

@Injectable()

export class EventService {
  baseURL = 'http://localhost:5011/v1/events';
  constructor(private http: HttpClient) { }

  public getEvents() : Observable<EventModel[]>
  {
    return this.http.get<EventModel[]>(this.baseURL)
  }

  public getEventsByTheme(theme: string) : Observable<EventModel[]>
  {
    return this.http.get<EventModel[]>(`${this.baseURL}/${theme}/theme`)
  }

  public getEventById(id: number) : Observable<EventModel>
  {
    return this.http.get<EventModel>(`${this.baseURL}/${id}`)
  }

  public postEvent(event: EventModel) : Observable<EventModel>
  {
    return this.http.post<EventModel>(this.baseURL, event);
  }

  public putEvent(id: number, event: EventModel) : Observable<EventModel>
  {
    return this.http.put<EventModel>(`${this.baseURL}/${id}`, event)
  }

  public deleteEvent(id: number): Observable<any>
  {
    return this.http.delete(`${this.baseURL}/${id}`)
  }

}
