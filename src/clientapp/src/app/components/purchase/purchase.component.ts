import { Component, OnInit } from '@angular/core';
import { ExchangeServiceService } from 'src/app/services/exchange-service/exchange-service.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit {
  isoCode: string = 'USD';
  userId: number = 1;
  amountArs: number = 1000;
  busy: boolean = false;
  purchaseRet : any = null;
  errorMsg: string = '';

  constructor(private exchangeRateService : ExchangeServiceService) { }

  ngOnInit(): void {
  }

  purchase() {
    this.busy = true;
    this.exchangeRateService
        .purchase(this.isoCode, this.userId, this.amountArs)
        .subscribe( (ret: any)=> {
          this.busy = false;
          this.errorMsg = '';
          this.purchaseRet = ret.Data;
          console.log('Subscribe callback:', this.purchaseRet);
        }, 
        error => {
          this.busy = false;
          this.errorMsg = error;
        });
  }
}
