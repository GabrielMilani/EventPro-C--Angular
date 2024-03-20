import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SocialNetworkModel } from '../models/SocialNetworkModel';
import { Observable, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SocialNetworkService {

  baseURL = environment.apiURL + 'v1/socialnetworks';

  constructor(private http: HttpClient) {}

  public getSocialNetworks(origin: string, id: number): Observable<SocialNetworkModel[]> {
    let URL =
      id === 0
        ? `${this.baseURL}/${origin}`
        : `${this.baseURL}/${origin}/${id}`;

    return this.http
    .get<SocialNetworkModel[]>(URL)
    .pipe(take(1));
  }

  public saveSocialNetwork(origin: string, id: number, socialNetworks: SocialNetworkModel[]): Observable<SocialNetworkModel[]> {
    let URL =
      id === 0
        ? `${this.baseURL}/${origin}`
        : `${this.baseURL}/${origin}/${id}`;

    return this.http
    .put<SocialNetworkModel[]>(URL, socialNetworks)
    .pipe(take(1));
  }

  public deleteSocialNetwork(origin: string, id: number, SocialNetworkId: number ): Observable<any> {
    let URL =
      id === 0
        ? `${this.baseURL}/${origin}/${SocialNetworkId}`
        : `${this.baseURL}/${origin}/${id}/${SocialNetworkId}`;

    return this.http
    .delete(URL)
    .pipe(take(1));
  }

}
