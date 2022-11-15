import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { Customer } from '../_models/customer';

import { environment } from '../environments/environment';
import { CartItem } from '../_models/cartItem';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  apiUrl = environment.apiUrl + '/Customer';
  apiUrl1 = environment.apiUrl + '/Customer/register';


  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient
  ) { }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl)
  }
  addCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.apiUrl1, customer, this.httpOptions);
  }

  updateCustomer(customerId: number, customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(`${this.apiUrl}/${customerId}`, customer, this.httpOptions);
  }

  deleteCustomer(customerId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${customerId}`, this.httpOptions);
  }
  
  
 
}
