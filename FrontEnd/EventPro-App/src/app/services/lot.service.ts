import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { LotModel } from '../models/LotModel';
import { environment } from '../../environments/environment';

@Injectable()
export class LotService {

  baseURL = environment.apiURL+'v1/lots';
  constructor(private http: HttpClient) { }

  public getLotsByEventId(eventId: number) : Observable<LotModel[]>
  {
    return this.http
    .get<LotModel[]>(`${this.baseURL}/${eventId}`)
    .pipe(take(1));
  }

  public getLotByIds(eventId: number, id: number) : Observable<LotModel>
  {
    return this.http
    .get<LotModel>(`${this.baseURL}/${eventId}/${id}`)
    .pipe(take(1));
  }

  public saveLot(eventId: number, lots: LotModel[]) : Observable<LotModel[]>
  {
    return this.http
    .put<LotModel[]>(`${this.baseURL}/${eventId}`, lots)
    .pipe(take(1));
  }

  public deleteLotByIds(eventId: number, lotId: number): Observable<any>
  {
    return this.http
    .delete(`${this.baseURL}/${eventId}/${lotId}`)
    .pipe(take(1));
  }

}
