/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SocialNetworkService } from './socialNetwork.service';

describe('Service: SocialNetwork', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SocialNetworkService]
    });
  });

  it('should ...', inject([SocialNetworkService], (service: SocialNetworkService) => {
    expect(service).toBeTruthy();
  }));
});
