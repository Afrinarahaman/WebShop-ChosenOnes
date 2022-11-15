import { Component, OnInit } from '@angular/core';

import { FormGroup, FormControl,FormBuilder } from '@angular/forms';

import { CustomerService } from '../_services/customer.service';
import { Customer} from '../_models/customer';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  customers: Customer[] = [];
  customer: Customer = this.newCustomer();
  message: string[] = [];
  error = '';
 

  

  constructor(
    private customerService: CustomerService,
   
  ) { }

  ngOnInit(): void {
    this.getCustomers();
  }

  newCustomer(): Customer {
    return { id: 0, email: '', password: '', firstName: '', lastName: '', address: '', telephone: ''};
  }

  getCustomers(): void {
    this.customerService.getCustomers()
      .subscribe(a => this.customers = a);
  }

  cancel(): void {
    this.message = [];
    this.customer = this.newCustomer();
  }

  save(): void {
    this.message = [];

    if (this.customer.email == '') {
      this.message.push('Email field cannot be empty');
    }
    if (this.customer.password == '') {
      this.message.push('Password field cannot be empty');
    }
    if (this.customer.firstName == '') {
      this.message.push('Enter Username');
    }
    if (this.customer.lastName == '') {
      this.message.push('Enter Lastname');
    }
    if (this.customer.address == '') {
      this.message.push('Enter Address');
    }
    if (this.customer.telephone == '') {
      this.message.push('Enter Telephone');
    }
  
    if (this.message.length == 0) {
      if (this.customer.id == 0) {
        this.customerService.addCustomer(this.customer)
          .subscribe({
            next: a => {
            this.customers.push(a)
            this.customer = this.newCustomer();
            alert('Thanks for Signing Up!');
           },
           error: (err)=>{
                        alert("User already exists!");
          }
        });
      } else {
            this.customerService.updateCustomer(this.customer.id, this.customer)
              .subscribe(() => {
                this.customer = this.newCustomer();
              });
           }
  }}
}
      
  
