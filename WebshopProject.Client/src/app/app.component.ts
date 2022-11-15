import { Component } from '@angular/core';
import { Category } from './_models/category';
import { CartService } from './_services/cart.service';
import { Router } from '@angular/router';
import { AuthService } from './_services/auth.service';

import { CategoryService } from './_services/category.service';
import { Role, Customer } from './_models/customer';
;
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentCustomer: Customer = { id: 0, email: '', password: '', firstName: '', lastName: '', address: '', telephone: ''};

  title = 'WebshopProject-Client';
  categories: Category[]=[];
  category:Category = {id: 0, categoryName :""};
  categoryId:number =0;
  public searchTerm:string="";

  public totalItem : number =this.cartService.getBasket().length;
  


  constructor(
    private router: Router,
    private authService: AuthService,
    private categoryService: CategoryService,
    private cartService: CartService
  ) {
    // get the current customer from authentication service
    this.authService.currentCustomer.subscribe(x => this.currentCustomer = x);
  }


  ngOnInit(): void{

    this.categoryService.getCategoriesWithoutProducts().subscribe(x => this.categories = x);
    console.log('value received ', );

  }

  logout() {
    if (confirm('Are you sure you want to log out?')) {
      // ask authentication service to perform logout
      this.authService.logout();
      

      // subscribe to the changes in currentUser, and load Home component
      this.authService.currentCustomer.subscribe(x => {
        this.currentCustomer = x
        this.router.navigate(['/']);
      });
    }
  }
  search(event:any){
    this.searchTerm=(event.target as HTMLInputElement).value;
    console.log(this.searchTerm);
    this.cartService.search.next(this.searchTerm);
  }

}

