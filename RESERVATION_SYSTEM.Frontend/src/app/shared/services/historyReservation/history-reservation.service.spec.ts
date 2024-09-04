import { TestBed } from '@angular/core/testing';

import { HistoryReservationService } from './history-reservation.service';

describe('HistoryReservationService', () => {
  let service: HistoryReservationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HistoryReservationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
