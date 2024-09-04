import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { CreateOrUpdateService } from '../../interfaces/createOrUpdateService.interface';
import { FieldFilter } from '../../interfaces/FieldFilter.interface';
import { Options } from '../../interfaces/options.interface';
import { Service } from '../../interfaces/Service.interface';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {

  constructor(private httpService: HttpService) { }

  public getService(data: FieldFilter[]): Observable<Service[]> {    
    return this.httpService.doPost<FieldFilter[],Service[]>(
      `${environment.endpoint_api_service}/list`,
      data
    );
  }

  public createService(createService: CreateOrUpdateService): Observable<unknown> {
    return this.httpService.doPost<CreateOrUpdateService,unknown>(
      `${environment.endpoint_api_service}`,
      createService
    );
  }
  
  public updateService(createService: CreateOrUpdateService): Observable<unknown> {
    return this.httpService.doPut<CreateOrUpdateService,unknown>(
      `${environment.endpoint_api_service}`,
      createService
    );
  }
  
  public deleteService(id: string): Observable<unknown> {
    const opts : Options = {
      body: {id}
    }
    return this.httpService.doDelete<unknown>(
      `${environment.endpoint_api_service}`,
      opts
    );
  }
}
