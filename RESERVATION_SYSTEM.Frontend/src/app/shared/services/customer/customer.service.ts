import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { CreateOrUpdateUser } from '../../interfaces/CreateUser';
import { FieldFilter } from '../../interfaces/FieldFilter.interface';
import { Options } from '../../interfaces/options.interface';
import { User } from '../../interfaces/user.interface';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpService: HttpService) { }

  public getUser(data: FieldFilter[]): Observable<User[]> {    
    return this.httpService.doPost<FieldFilter[],User[]>(
      `${environment.endpoint_api_customer}/list`,
      data
    );
  }

  public createUser(createUser: CreateOrUpdateUser): Observable<unknown> {
    return this.httpService.doPost<CreateOrUpdateUser,unknown>(
      `${environment.endpoint_api_customer}`,
      createUser
    );
  }
  
  public updateUser(createUser: CreateOrUpdateUser): Observable<unknown> {
    return this.httpService.doPut<CreateOrUpdateUser,unknown>(
      `${environment.endpoint_api_customer}`,
      createUser
    );
  }
  
  public deleteUser(id: string): Observable<unknown> {
    const opts : Options = {
      body: {id}
    }
    return this.httpService.doDelete<unknown>(
      `${environment.endpoint_api_customer}`,
      opts
    );
  }
}
