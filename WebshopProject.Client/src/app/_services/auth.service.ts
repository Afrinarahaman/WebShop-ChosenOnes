import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../environments/environment';

import { Role, Customer} from '../_models/customer';
import { Data, Router } from '@angular/router';



@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentCustomerSubject: BehaviorSubject<Customer>;
  public currentCustomer: Observable<Customer>;

  constructor(private router: Router, private http: HttpClient) {
    // fake login durring testing
    // if (sessionStorage.getItem('currentUser') == null) {
    //   sessionStorage.setItem('currentUser', JSON.stringify({ id: 0, email: '', customername: '', role: null }));
    // }
    this.currentCustomerSubject = new BehaviorSubject<Customer>(JSON.parse(sessionStorage.getItem('currentCustomer') as string));
    this.currentCustomer = this.currentCustomerSubject.asObservable();
  }

  public get currentCustomerValue(): Customer {
    return this.currentCustomerSubject.value;
  }

  login(email: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/Customer/authenticate`, { email, password })
      .pipe(map(customer => {
        // store customer details and jwt token in local storage to keep customer logged in between page refreshes
        sessionStorage.setItem('currentCustomer', JSON.stringify(customer));
        
        this.currentCustomerSubject.next(customer);
        // console.log('login customer',customer);
        return customer;
      }));
  }

  logout() {
    // remove customer from local storage to log customer out
    sessionStorage.removeItem('currentCustomer');
    // reset CurrentUserSubject, by fetching the value in sessionStorage, which is null at this point
    this.currentCustomerSubject = new BehaviorSubject<Customer>(JSON.parse(sessionStorage.getItem('currentCustomer') as string));
    // reset CurrentUser to the resat UserSubject, as an obserable
    this.currentCustomer = this.currentCustomerSubject.asObservable();
  }



 register(email: string, password: string, firstName: string, LastName: string, address: string, telephone: string) {
    return this.http.post<any>(`${environment.apiUrl}/Customer/authenticate`, { email, password, firstName, LastName, address, telephone})
      .pipe(map(customer => {
        // store customer details and jwt token in local storage to keep customer logged in between page refreshes
        sessionStorage.setItem('currentCustomer', JSON.stringify(customer));
        this.currentCustomerSubject.next(customer);
        // console.log('login customer',customer);
        return customer;
      }));
  }
}
