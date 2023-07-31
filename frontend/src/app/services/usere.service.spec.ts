import { TestBed } from '@angular/core/testing';

import { UsereService } from './usere.service';

describe('UsereService', () => {
  let service: UsereService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UsereService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
