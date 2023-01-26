/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SharedFormService } from './sharedForm.service';

describe('Service: SharedForm', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SharedFormService]
    });
  });

  it('should ...', inject([SharedFormService], (service: SharedFormService) => {
    expect(service).toBeTruthy();
  }));
});
