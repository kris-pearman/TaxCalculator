import { Component, signal } from '@angular/core';
import { TaxCalculator } from '../../models/taxCalculator.model';

@Component({
  selector: 'app-tax-display',
  standalone: false,
  
  templateUrl: './tax-display.component.html',
  styleUrl: './tax-display.component.css'
})
export class TaxDisplayComponent {
  salary = signal(0);

  calculatedTax: TaxCalculator = {
  annualGrossSalary: 0,
  annualNetSalary: 0,
  annualTaxPaid: 0,
  monthlyGrossSalary: 0,
  monthlyNetSalary: 0,
  monthlyTaxPaid: 0
  };

  ngOnChanges(salary: number)
  {
    //make API call here
    //something like this.apiService.calculateTax(salary).subscribe(
    //(response) => {this.calculatedTax = response
    //},
    //(error) => {
    //Error handling here, set loading to false and set variable to display error message?
    //})
  }
}
