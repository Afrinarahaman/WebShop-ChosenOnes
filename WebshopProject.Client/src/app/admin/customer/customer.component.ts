
import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/_models/customer';
import { CustomerService } from 'src/app/_services/customer.service';


@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  customers: Customer[] = [];
  customer: Customer = this.newCustomer();
  message: string[] = [];

  constructor(private customerService: CustomerService) { }

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

  edit(customer: Customer): void {
    this.customer = customer;
    this.message = [];
  }

  delete(customer: Customer): void {
    if (confirm('Are you sure you want to delete?')) {
      this.customerService.deleteCustomer(customer.id)
        .subscribe(() => {
          this.customers = this.customers.filter(a => a.id != customer.id)
        });
    }
  }

  cancel(): void {
    this.message = [];
    this.customer = this.newCustomer();
    this.getCustomers();
  }

  save(): void {
    this.message = [];

    if (this.customer.email == '') {
      this.message.push('Enter Email');
    }
   if (this.customer.password == '') {
      this.message.push('Enter Password');
    }
    if (this.customer.firstName == '') {
      this.message.push('Enter FirstName');
    }
    if (this.customer.lastName == '') {
      this.message.push('Enter LastName');
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
          .subscribe(a => {
            this.customers.push(a)
            this.customer = this.newCustomer();
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
