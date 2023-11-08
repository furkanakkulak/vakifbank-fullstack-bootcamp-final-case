import { TestBed } from '@angular/core/testing';

import { AdminDealerService } from './admin-dealer.service';

describe('AdminDealerService', () => {
  let service: AdminDealerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdminDealerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
