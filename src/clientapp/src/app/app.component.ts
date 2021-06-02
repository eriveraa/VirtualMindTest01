import { Component } from '@angular/core';
import { ExchangeServiceService } from './services/exchange-service/exchange-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClientApp';
  errorMessage:string = '';
  busy = false;
  isoCodeForInput = 'USD';
  isoCodeForRequest = '';
  exchangeRate: any = null;

  constructor(private exchangeRateService : ExchangeServiceService) {
  }

  getExchangeRate() {
    this.isoCodeForRequest = this.isoCodeForInput;    
    this.busy = true;
    this.exchangeRateService.getExchangeRate(this.isoCodeForRequest)
      .subscribe( (ret: any)=> {
        this.busy = false;
        this.exchangeRate = ret.Data;
        console.log(ret);
      });
   }
}
