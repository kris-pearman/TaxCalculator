import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalculationPageComponent } from './calculation-page.component';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { PaySummaryService } from '../../services/paySummary.service';
import { FormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';

describe('CalculationPageComponent', () => {
  let component: CalculationPageComponent;
  let fixture: ComponentFixture<CalculationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CalculationPageComponent],
      imports: [FormsModule],
      providers: [
        PaySummaryService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CalculationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialise with default values', () => {
    expect(component.errorMessage).toBe('');
    expect(component.isLoading).toBeFalse;
    expect(component.salary).toBe(0);
    expect(component.showSummary).toBeFalse;
  });

  it('should show an error if salary is not an integer', () => {
    component.salary = 123.45;
    component.onClickCalculate();
    fixture.detectChanges();

    expect(component.errorMessage).toBe('Salary needs to be a whole number.');
    expect(component.isLoading).toBeFalse;
    const errorElement = fixture.nativeElement.querySelector('.text-danger');
    expect(errorElement).toBeTruthy();
    expect(errorElement.textContent).toContain(
      'Salary needs to be a whole number.'
    );
  });
});
