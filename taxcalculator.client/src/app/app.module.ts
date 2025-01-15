import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CalculationPageComponent } from './pages/calculation-page/calculation-page.component';
import { TaxDisplayComponent } from './components/tax-display/tax-display.component';

@NgModule({
  declarations: [
    AppComponent,
    CalculationPageComponent,
    TaxDisplayComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
