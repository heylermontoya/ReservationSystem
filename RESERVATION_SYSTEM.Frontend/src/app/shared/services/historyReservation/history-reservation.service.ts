import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { FieldFilter } from '../../interfaces/FieldFilter.interface';
import { HistoryReservations } from '../../interfaces/historyReservations.interface';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class HistoryReservationService {

  constructor(private httpService: HttpService) { }

  public getHistoryReservation(data: FieldFilter[]): Observable<HistoryReservations[]> {    
    return this.httpService.doPost<FieldFilter[],HistoryReservations[]>(
      `${environment.endpoint_api_history_reservation}/list`,
      data
    );
  }
}
