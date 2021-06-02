import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PurchaseComponent } from './components/purchase/purchase.component';
import { ViewExchangeRatesComponent } from './components/view-exchange-rates/view-exchange-rates.component';

const routes: Routes = [
  { path: '', redirectTo: 'quote', pathMatch: 'full'},
  { path: 'quote', component: ViewExchangeRatesComponent },
  { path: 'purchase', component: PurchaseComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
