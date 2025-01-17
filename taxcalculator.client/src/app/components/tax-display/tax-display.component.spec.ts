import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxDisplayComponent } from './tax-display.component';

describe('TaxDisplayComponent', () => {
  let component: TaxDisplayComponent;
  let fixture: ComponentFixture<TaxDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TaxDisplayComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TaxDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
