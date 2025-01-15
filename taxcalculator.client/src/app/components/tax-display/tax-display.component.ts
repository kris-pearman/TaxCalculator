import { Component, Input } from '@angular/core';
import { TaxCalculator } from '../../models/taxCalculator.model';

@Component({
  selector: 'app-tax-display',
  standalone: false,

  templateUrl: './tax-display.component.html',
  styleUrl: './tax-display.component.css',
})
export class TaxDisplayComponent {
  @Input() taxValues!: TaxCalculator | undefined;
}
