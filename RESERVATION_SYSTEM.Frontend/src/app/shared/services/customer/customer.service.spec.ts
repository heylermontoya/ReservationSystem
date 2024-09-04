import { TestBed } from '@angular/core/testing';
import { CustomerService } from './customer.service';
import {HttpTestingController, HttpClientTestingModule} from "@angular/common/http/testing";
import { HttpService } from '../http-service/http.service';
import { environment } from '../../../../environments/environment';
import { CustomerOrderBuilder } from '../../../../../mocks/CustomerOrderBuilder';

describe('CustomerService', () => {
  let service: CustomerService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HttpService,CustomerService]
    });
    service = TestBed.inject(CustomerService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should getCustomerOrder', () => {
    // Arrange
    const searchTerm = '';
    const customerOrder = new CustomerOrderBuilder().build();
    const mockOrders = [customerOrder];

    const url = `${environment.endpoint_api_customer}`;

    // Act
    service.getCustomerOrder(searchTerm).subscribe((orders) => {
      expect(orders).toEqual(mockOrders); 
    });

    // Assert
    const req = httpMock.expectOne(req => {
      return req.url === url && req.method === 'GET' && req.params.has('Search') === false;
    });

    expect(req.request.method).toBe('GET');
    expect(req.request.params.has('Search')).toBe(false);

    req.flush(mockOrders); 
  });

  it('should getCustomerOrder with searchTerm and add Search parameter', () => {
    // Arrange
    const searchTerm = 'John';
    const customerOrder = new CustomerOrderBuilder().build();
    const mockOrders = [customerOrder];
    const url = `${environment.endpoint_api_customer}`;

    // Act
    service.getCustomerOrder(searchTerm).subscribe((orders) => {
      expect(orders).toEqual(mockOrders); 
    });

    // Assert
    const req = httpMock.expectOne(req => {
      return req.url === url && req.method === 'GET' && req.params.has('Search') === true && req.params.get('Search') === searchTerm;
    });

    expect(req.request.method).toBe('GET');
    expect(req.request.params.has('Search')).toBe(true); 
    expect(req.request.params.get('Search')).toBe(searchTerm); 

    req.flush(mockOrders); 
  });
});
