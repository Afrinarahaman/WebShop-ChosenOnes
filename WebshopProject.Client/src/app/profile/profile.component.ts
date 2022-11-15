import { Customer } from '../_models/customer';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { CustomerService } from '../_services/customer.service';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']

})
export class ProfileComponent implements OnInit {

  customers: Customer[] = [];
  customer: Customer = this.newCustomer();
  message: string[] = [];
  currentCustomer: Customer = { id: 0, email: '', password: '', firstName: '', lastName: '', address: '', telephone: ''};


  constructor(
    private router: Router,
    private authService: AuthService,
    private customerService: CustomerService

  ) {
    // get the current customer from authentication service
    this.authService.currentCustomer.subscribe(x => this.currentCustomer = x);
  }

  ngOnInit(): void {

  }

  newCustomer(): Customer {
    return { id: 0, email: '', password: '', firstName: '', lastName: '', address: '', telephone: ''};
  }

  edit(customer: Customer): void {
    this.customer=  customer;
    this.message = [];
  }

  cancel(): void {
    this.message = [];
    this.customer= this.newCustomer();
  }

  save(): void {
    if (this.customer.email != '') {
     (confirm('To view the updated profile kindly "Sign in" again....!'))
    }
    this.message = [];

    if (this.customer.email == '') {
      this.message.push('Enter Email');
    }

    if (this.customer.password == '') {
      this.message.push('Enter Password');
    }

    if (this.customer.firstName == '') {
      this.message.push('Enter Firstname');
    }

    if (this.customer.lastName == '') {
      this.message.push('Enter lastname');
    }

    if (this.customer.address== '') {
      this.message.push('Enter address');
    }

    if (this.customer.telephone == '') {
      this.message.push('Enter telephone');
    }

    if (this.message.length == 0) {
      if (this.customer.id == 0) {
        this.customerService.addCustomer(this.customer)
           .subscribe(a => {
          this.customers.push(a)
          this.customer= this.newCustomer();
          });
      } else {
        this.customerService.updateCustomer(this.customer.id, this.customer)
          .subscribe(() => {
            this.customer = this.newCustomer();
          });
      }
    }
  }
}
