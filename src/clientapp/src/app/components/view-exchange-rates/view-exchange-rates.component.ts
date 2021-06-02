import { Component, OnInit } from '@angular/core';
import { ExchangeServiceService } from '../../services/exchange-service/exchange-service.service';

@Component({
  selector: 'app-view-exchange-rates',
  templateUrl: './view-exchange-rates.component.html',
  styleUrls: ['./view-exchange-rates.component.css']
})
export class ViewExchangeRatesComponent implements OnInit {
  busy = false;
  exchangeRateUSD: any;
  exchangeRateBRL: any;
  errorMsg: string = '';

  constructor(private exchangeRateService : ExchangeServiceService) { }

  ngOnInit(): void {
    this.refreshExchangeRates();
  }

  refreshExchangeRates() {
    this.busy = true;

    // Get data for USD
    this.exchangeRateService.getExchangeRate('USD')
      .subscribe( (ret: any)=> {
        this.busy = false;
        this.exchangeRateUSD = ret.Data.Ammount;
        this.errorMsg = '';
        console.log('USD ok');
      }, 
      error => {
        this.busy = false;
        this.errorMsg = error;
        console.log('USD error');
      });
  
    // Get data for BRL
    this.exchangeRateService.getExchangeRate('BRL')
    .subscribe( (ret: any)=> {
      this.busy = false;
      this.exchangeRateBRL = ret.Data.Ammount;
      this.errorMsg = '';
      console.log('BRL ok');
    }, 
    error => {
      this.busy = false;
      this.errorMsg = error;
      console.log('BRL error');
    });
  }

}
