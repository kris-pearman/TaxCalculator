import { Component, inject } from '@angular/core';
import { PaySummaryService } from '../../services/paySummary.service';
import { PaySummary } from '../../models/paySummary.model';
import { finalize } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

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
  taxService = inject(PaySummaryService);
  taxValues: PaySummary | undefined;

  onClickCalculate() {
    this.errorMessage = '';
    this.isLoading = true;
    if (!Number.isInteger(this.salary)) {
      this.errorMessage = `Salary needs to be a whole number.`;
      this.isLoading = false;
      return;
    }
    this.retrieveTax();
  }

  private retrieveTax() {
    this.taxService
      .create(this.salary)
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: (result: PaySummary) => {
          this.taxValues = result;
        },
        error: (err: HttpErrorResponse) => {
          if (err.status == 400) {
            this.errorMessage = 'Salary needs to be a positive number';
          } else if (err.status == 500) {
            this.errorMessage =
              'Internal server error. Please try again later.';
          } else {
            this.errorMessage = `Error fetching tax details: ${err.message}`;
          }
        },
      });
    this.showSummary = true;
  }
}
