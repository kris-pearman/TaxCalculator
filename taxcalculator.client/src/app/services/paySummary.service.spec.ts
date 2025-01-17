import { TestBed } from '@angular/core/testing';

import { PaySummaryService } from './paySummary.service';
import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { PaySummary as PaySummary } from '../models/paySummary.model';

describe('PaySummaryService', () => {
  let service: PaySummaryService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(PaySummaryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return pay summary data when the endpoint is called', () => {
    const mockPaySummary: PaySummary = {
      annualGrossSalary: 60000,
      annualNetSalary: 56000,
      annualTaxPaid: 4000,
      monthlyGrossSalary: 5000,
      monthlyNetSalary: 4450,
      monthlyTaxPaid: 350,
    };

    service.create(60000).subscribe((data) => {
      expect(data).toEqual(mockPaySummary);
    });

    const req = httpMock.expectOne('/taxcalculator/60000');
    expect(req.request.method).toBe('GET');
    req.flush(mockPaySummary);
  });

  it('should handle a bad request error', () => {
    const errorMessage = 'Bad Request';

    service.create(-100).subscribe({
      next: () => fail('no error was returned'),
      error: (error) => {
        expect(error.status).toBe(400);
        expect(error.statusText).toBe(errorMessage);
      },
    });

    const req = httpMock.expectOne('/taxcalculator/-100');
    req.flush(errorMessage, { status: 400, statusText: errorMessage });
  });

  it('should handle an internal server error', () => {
    const errorMessage = 'Bad Request';

    service.create(-100).subscribe({
      next: () => fail('no error was returned'),
      error: (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe(errorMessage);
      },
    });

    const req = httpMock.expectOne('/taxcalculator/-100');
    req.flush(errorMessage, { status: 500, statusText: errorMessage });
  });
});
