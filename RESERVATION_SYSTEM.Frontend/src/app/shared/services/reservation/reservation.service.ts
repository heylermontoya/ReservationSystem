import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { CancelReservation } from '../../interfaces/cancelReservation.interface';
import { CreateOrUpdateReservation } from '../../interfaces/createOrUpdateReservation.interface';
import { FieldFilter } from '../../interfaces/FieldFilter.interface';
import { Reservation } from '../../interfaces/reservation.interface';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private httpService: HttpService) { }

  public getReservation(data: FieldFilter[]): Observable<Reservation[]> {    
    return this.httpService.doPost<FieldFilter[],Reservation[]>(
      `${environment.endpoint_api_reservation}/list`,
      data
    );
  }

  public createReservation(createReservation: CreateOrUpdateReservation): Observable<unknown> {
    return this.httpService.doPost<CreateOrUpdateReservation,unknown>(
      `${environment.endpoint_api_reservation}`,
      createReservation
    );
  }
  
  public updateReservation(createReservation: CreateOrUpdateReservation): Observable<unknown> {
    return this.httpService.doPut<CreateOrUpdateReservation,unknown>(
      `${environment.endpoint_api_reservation}`,
      createReservation
    );
  }
  
  public deleteReservation(id: string): Observable<unknown> {
    const opts : CancelReservation = {
      Id:id
    }
    return this.httpService.doPut<CancelReservation,unknown>(
      `${environment.endpoint_api_reservation}/cancel`,
      opts
    );
  }
}
