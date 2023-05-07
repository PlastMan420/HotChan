/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { HotchanService } from './hotchan.service';

describe('Service: Hotchan', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HotchanService]
    });
  });

  it('should ...', inject([HotchanService], (service: HotchanService) => {
    expect(service).toBeTruthy();
  }));
});
