import { TestBed, inject } from '@angular/core/testing';

import { SecuritiesService } from './securities.service';

describe('SecuritiesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SecuritiesService]
    });
  });

  it('should be created', inject([SecuritiesService], (service: SecuritiesService) => {
    expect(service).toBeTruthy();
  }));
});
