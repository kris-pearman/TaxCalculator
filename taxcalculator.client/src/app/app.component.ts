import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TaxCalculator } from './models/taxCalculator.model';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  public taxValues: TaxCalculator = {
    annualGrossSalary: 0,
    annualNetSalary: 0,
    annualTaxPaid: 0,
    monthlyGrossSalary: 0,
    monthlyNetSalary: 0,
    monthlyTaxPaid: 0,
  };
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getTax();
  }

  getTax() {
    this.http.get<TaxCalculator>('/taxcalculator/100000').subscribe(
      (result) => {
        console.log('API Response:', result);
        this.taxValues = result;
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  title = 'taxcalculator.client';
}
