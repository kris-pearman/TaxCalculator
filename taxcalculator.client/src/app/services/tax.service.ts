import { Injectable } from '@angular/core';
import { TaxCalculator } from '../models/taxCalculator.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TaxService {
  constructor(private http: HttpClient) {}

  calculateTax(salary: number) {
    return this.http.get<TaxCalculator>(`/taxcalculator/${salary}`);
  }
}
//TODO: CHANGE ENDPOINT WHEN WE REFACTOR BACKEND
