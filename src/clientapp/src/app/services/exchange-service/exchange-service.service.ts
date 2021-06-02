import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

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
    return this.http.get(url)
            .pipe(
              catchError(this.handleError)
            );
  }

  purchase(isoCode : string, userId: number, amountArs: number): Observable<any> {
    var url = this.baseUrl + "purchase";
    console.log('Sending POST to', url);
    var payload = { 
      UserId: userId, 
      AmmountARS: amountArs, 
      IsoCurrencyCode: isoCode
    };
    return this.http.post(url, payload)
            .pipe(
              catchError(this.handleError)
            );
  }

  handleError(errorResponse: HttpErrorResponse) {
    console.log('ErrorResponse:', errorResponse)
    let errorMessage: string;
    if (!errorResponse.error) {
      errorMessage = `Unknown Error ${errorResponse.status}: ${errorResponse.message}`;
    }
    else if (errorResponse.error instanceof ProgressEvent) {
      errorMessage = `Client Error ${errorResponse.status}: ${errorResponse.message}`;
    } 
    else {
        errorMessage = `Server Error ${errorResponse.status}: ${errorResponse.error.serverErrorMessage}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }

}

