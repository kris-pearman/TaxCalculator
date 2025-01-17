import { Component, Input } from '@angular/core';
import { PaySummary } from '../../models/paySummary.model';

@Component({
  selector: 'app-tax-display',
  standalone: false,

  templateUrl: './tax-display.component.html',
  styleUrl: './tax-display.component.css',
})
export class TaxDisplayComponent {
  @Input() taxValues!: PaySummary | undefined;
}
