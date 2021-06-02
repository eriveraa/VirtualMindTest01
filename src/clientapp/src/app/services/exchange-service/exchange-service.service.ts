import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExchangeServiceService {
  errorMessage:string = '';
  baseUrl = "https://localhost:5001/api/ExchangeRate/";

  constructor(private http : HttpClient) {    
  }

   getExchangeRate(isoCode : string): Observable<any> {
    var url = this.baseUrl + "getexchangetoars/" + isoCode;
    console.log('Sending GET to', url);
    return this.http.get(url);
  }
}

