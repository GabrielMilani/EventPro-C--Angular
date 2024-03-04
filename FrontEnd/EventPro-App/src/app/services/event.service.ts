import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EventModel } from '../models/EventModel';

@Injectable()

export class EventService {
  baseURL = 'http://localhost:5185/v1/events';
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
}
