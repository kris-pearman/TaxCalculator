import { Component, output } from '@angular/core';

@Component({
  selector: 'app-calculation-page',
  standalone: false,
  
  templateUrl: './calculation-page.component.html',
  styleUrl: './calculation-page.component.css'
})
export class CalculationPageComponent {
  salary: number = 0;

  submitSalaryHandler(salary: number) {
    //code to enable the new component here?
  }

  onClickHandler()
  {
    console.log("Click")
  }
}
