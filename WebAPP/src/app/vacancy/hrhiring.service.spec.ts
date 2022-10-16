import { TestBed } from '@angular/core/testing';

import { HRHiringService } from './hrhiring.service';

describe('HRHiringService', () => {
  let service: HRHiringService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HRHiringService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
