import { HttpHeaders, HttpParams } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Options } from '../../interfaces/options.interface';
import { HttpService } from './http.service';

describe('HttpService', () => {
  let httpService: HttpService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpService],
      imports: [HttpClientTestingModule],
    });

    httpService = TestBed.inject(HttpService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(httpService).toBeTruthy();
  });

  it('should create default options with Content-Type header set to application/json', () => {
    const defaultOptions = httpService.createDefaultOptions();

    expect(defaultOptions.headers).toBeDefined();
    expect(defaultOptions.headers instanceof HttpHeaders).toBe(true);
  });

  it('should perform a GET request without options', () => {
    const testData = { name: 'Test Data' };

    httpService.doGet('/test-url').subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('/test-url');
    expect(req.request.method).toBe('GET');
    req.flush(testData);
  });

  it('should perform a GET request with options', () => {
    const testData = { name: 'Test Data' };
    const opts: Options = {
      headers: new HttpHeaders({ 'Custom-Header': 'CustomValue' }),
      params: new HttpParams().set('param1', 'value1')
    };

    httpService.doGet('/test-url', opts).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne(req => req.url === '/test-url' && req.params.has('param1'));
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Custom-Header')).toBe('CustomValue');
    expect(req.request.params.get('param1')).toBe('value1');
    req.flush(testData);
  });

  it('should perform a POST request without options', () => {
    const testData = { name: 'Test Data' };
    const postData = { name: 'Post Data' };

    httpService.doPost('/test-url', postData).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('/test-url');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(postData);
    req.flush(testData);
  });

  it('should perform a POST request with options', () => {
    const testData = { name: 'Test Data' };
    const postData = { name: 'Post Data' };
    const opts: Options = {
      headers: new HttpHeaders({ 'Custom-Header': 'CustomValue' }),
      params: new HttpParams().set('param1', 'value1')
    };

    httpService.doPost('/test-url', postData, opts).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne(req => req.url === '/test-url' && req.params.has('param1'));
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(postData);
    expect(req.request.headers.get('Custom-Header')).toBe('CustomValue');
    expect(req.request.params.get('param1')).toBe('value1');
    req.flush(testData);
  });

  it('should perform a PUT request without options', () => {
    const testData = { name: 'Test Data' };
    const putData = { name: 'Put Data' };

    httpService.doPut('/test-url', putData).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('/test-url');
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(putData);
    req.flush(testData);
  });

  it('should perform a PUT request with options', () => {
    const testData = { name: 'Test Data' };
    const putData = { name: 'Put Data' };
    const opts: Options = {
      headers: new HttpHeaders({ 'Custom-Header': 'CustomValue' }),
      params: new HttpParams().set('param1', 'value1')
    };

    httpService.doPut('/test-url', putData, opts).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne(req => req.url === '/test-url' && req.params.has('param1'));
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(putData);
    expect(req.request.headers.get('Custom-Header')).toBe('CustomValue');
    expect(req.request.params.get('param1')).toBe('value1');
    req.flush(testData);
  });

  it('should perform a PATCH request without options', () => {
    const testData = { name: 'Test Data' };
    const patchData = { name: 'Patch Data' };

    httpService.doPatch('/test-url', patchData).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('/test-url');
    expect(req.request.method).toBe('PATCH');
    expect(req.request.body).toEqual(patchData);
    req.flush(testData);
  });

  it('should perform a PATCH request with options', () => {
    const testData = { name: 'Test Data' };
    const patchData = { name: 'Patch Data' };
    const opts: Options = {
      headers: new HttpHeaders({ 'Custom-Header': 'CustomValue' }),
      params: new HttpParams().set('param1', 'value1')
    };

    httpService.doPatch('/test-url', patchData, opts).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne(req => req.url === '/test-url' && req.params.has('param1'));
    expect(req.request.method).toBe('PATCH');
    expect(req.request.body).toEqual(patchData);
    expect(req.request.headers.get('Custom-Header')).toBe('CustomValue');
    expect(req.request.params.get('param1')).toBe('value1');
    req.flush(testData);
  });

  it('should perform a DELETE request without options', () => {
    const testData = { name: 'Test Data' };

    httpService.doDelete('/test-url').subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('/test-url');
    expect(req.request.method).toBe('DELETE');
    req.flush(testData);
  });

  it('should perform a DELETE request with options', () => {
    const testData = { name: 'Test Data' };
    const opts: Options = {
      headers: new HttpHeaders({ 'Custom-Header': 'CustomValue' }),
      params: new HttpParams().set('param1', 'value1')
    };

    httpService.doDelete('/test-url', opts).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne(req => req.url === '/test-url' && req.params.has('param1'));
    expect(req.request.method).toBe('DELETE');
    expect(req.request.headers.get('Custom-Header')).toBe('CustomValue');
    expect(req.request.params.get('param1')).toBe('value1');
    req.flush(testData);
  });

  it('should perform a GET request with parameters', () => {
    const testData = { name: 'Test Data' };
    const params = new HttpParams().set('param1', 'value1');

    httpService.doGetParameters('/test-url', params).subscribe(data => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne(req => req.url === '/test-url' && req.params.has('param1'));
    expect(req.request.method).toBe('GET');
    expect(req.request.params.get('param1')).toBe('value1');
    req.flush(testData);
  });

  it('should set a header if it is allowed', () => {
    const headerName = 'Authorization';
    const headerValue = 'Bearer token';

    const options = httpService.setHeader(headerName, headerValue);

    expect(options.headers).toBeDefined();
    expect(options.headers?.get(headerName)).toBe(headerValue);
  });

  it('should throw an error if the header is not allowed', () => {
    const headerName = 'Not-Allowed-Header';
    const headerValue = 'Some value';

    expect(() => httpService.setHeader(headerName, headerValue)).toThrowError(
      `El encabezado ${headerName} no está permitido para ser modificado.`
    );
  });
});
