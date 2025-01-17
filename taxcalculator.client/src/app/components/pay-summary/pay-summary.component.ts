import { Component, Input } from '@angular/core';
import { PaySummary } from '../../models/paySummary.model';

@Component({
  selector: 'app-pay-summary',
  standalone: false,

  templateUrl: './pay-summary.component.html',
  styleUrl: './pay-summary.component.css',
})
export class PaySummaryComponent {
  @Input() taxValues!: PaySummary | undefined;
}
