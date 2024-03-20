import { SpeakerAddModel } from './../models/SpeakerAddModel';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SpeakerModel } from '../models/SpeakerModel';
import { PaginatedResult } from '../models/Pagination';
import { Observable, map, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpeakerService {

  baseURL = environment.apiURL+'v1/speakers';

  constructor(private http: HttpClient) { }

  public getSpeakers(page?: number, itemsPerPage?: number, term?: string) : Observable<PaginatedResult<SpeakerModel[]>>
  {
    const paginatedResult : PaginatedResult<SpeakerModel[]> = new PaginatedResult<SpeakerModel[]>();
    let params = new HttpParams;

    if (page != null && itemsPerPage != null){
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    if (term != null && term != ''){
      params = params.append('term', term)
    }

    return this.http
    .get<SpeakerModel[]>(this.baseURL + '/all', {observe: 'response', params})
    .pipe(take(1), map((response) =>{
      paginatedResult.result = response.body;
      if(response.headers.has('Pagination')){
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    }));
  }

  public getSpeaker() : Observable<SpeakerModel>
  {
    return this.http
    .get<SpeakerModel>(`${this.baseURL}`)
    .pipe(take(1));
  }

  public post() : Observable<SpeakerModel>
  {
    return this.http
    .post<SpeakerModel>(this.baseURL, {} as SpeakerModel )
    .pipe(take(1));
  }

  public put(speaker: SpeakerModel) : Observable<SpeakerModel>
  {
    return this.http
    .put<SpeakerModel>(`${this.baseURL}`, speaker)
    .pipe(take(1));
  }
}
