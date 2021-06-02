import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ViewExchangeRatesComponent } from './components/view-exchange-rates/view-exchange-rates.component'; 
import { PurchaseComponent } from './components/purchase/purchase.component';

@NgModule({
  declarations: [
    AppComponent,
    ViewExchangeRatesComponent,
    PurchaseComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
