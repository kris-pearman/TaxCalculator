import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Taxcalculator {
  annualGrossPay: number;
  annualNetPay: number;
  annuarlTaxPaid: number;
  monthlyGrossPay: number;
  monthlyNetPay: number;
  monthlyTaxPaid: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  public taxValues: Taxcalculator = {
    annualGrossPay: 0,
    annualNetPay: 0,
    annuarlTaxPaid: 0,
    monthlyGrossPay: 0,
    monthlyNetPay: 0,
    monthlyTaxPaid: 0,
  };
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
    this.getTax();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getTax() {
    this.http.get<Taxcalculator>('/taxcalculator/1').subscribe(
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
