import { Component, inject } from '@angular/core';
import { TaxService } from '../../services/tax.service';
import { TaxCalculator } from '../../models/taxCalculator.model';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-calculation-page',
  standalone: false,

  templateUrl: './calculation-page.component.html',
  styleUrl: './calculation-page.component.css',
})
export class CalculationPageComponent {
  salary: number = 0;
  isLoading: boolean = false;
  errorMessage: string = '';
  showSummary: boolean = false;
  taxService = inject(TaxService);
  taxValues: TaxCalculator | undefined;

  onClickCalculate() {
    this.isLoading = true;
    try {
      this.retrieveTax();
    } catch (err) {
      this.isLoading = false;
      this.errorMessage = `${err}`;
    }
  }

  private retrieveTax() {
    this.taxService
      .calculateTax(this.salary)
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: (result: TaxCalculator) => {
          this.taxValues = result;
        },
        error: (err: Error) => {
          this.errorMessage = `Error fetching tax details: ${err.message}`;
        },
      });
    this.showSummary = true;
  }
}
