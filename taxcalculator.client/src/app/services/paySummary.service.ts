import { Injectable } from '@angular/core';
import { PaySummary } from '../models/paySummary.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class PaySummaryService {
  constructor(private http: HttpClient) {}

  create(salary: number) {
    return this.http.get<PaySummary>(`/api/v1/PaySummary/${salary}`);
  }
}
