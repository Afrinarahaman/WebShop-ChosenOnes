import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../environments/environment';
import { Order } from '../_models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  apiUrl = environment.apiUrl + '/order';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(private http: HttpClient) { }

  storeOrder(newOrder: Order): Observable<Order> {
    // console.log("mandag");
    console.log("storeORder", newOrder);
    console.log(this.apiUrl);
    // return this.http.get<Order[]>(this.apiUrl);
   return this.http.post<Order>(this.apiUrl, newOrder, this.httpOptions)
  }

  getOrderDetailsByOrderId(orderId:number):Observable<any>{
    return this.http.get<Order>(`${this.apiUrl}/${orderId}`, this.httpOptions);
  }
}


