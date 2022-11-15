import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CartItem } from '../_models/cartItem';
import { Product } from '../_models/product';
import { AuthService } from '../_services/auth.service';
import { CartService } from '../_services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  public quantity: number = 0;

  public grandTotal: number = 0;
  public cartProducts: CartItem[] = [];  //property


  constructor(private cartService: CartService, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.cartProducts = this.cartService.getBasket();
    this.grandTotal = this.cartService.getTotalPrice();


  }

  async createOrder() {
    // let customerId=parseInt(this.authService.currentCustomerValue.id)
    if (this.authService.currentCustomerValue == null || this.authService.currentCustomerValue.id == 0) {
      alert("Do you have any account? If yes, then Login, otherwise create a new account..");
      this.router.navigate(['login']);
    }
    else {


      var result = await this.cartService.addOrder();
      console.log('result', result);
             this.cartService.clearBasket();    
           this.router.navigate(['/thankyou/'+result.id]);
           

    }
  }
  public basket = this.cartService.basket;

  removeItem(productId: number) {
    console.log(productId);
    if (confirm("are you sure to remove?")) {
      this.cartService.removeItemFromBasket(productId);


    }
    // this.cartProducts=this.cartService.getBasket();
    //   this.grandTotal= this.cartService.getTotalPrice();
    //   this.cartService.saveBasket();
    window.location.reload();
  }
  emptycart() {
    if (confirm("are u sure to remove?"))
      this.cartService.clearBasket();
    // this.cartProducts=this.cartService.getBasket();
    // this.grandTotal= this.cartService.getTotalPrice();
    // this.cartService.saveBasket();
    window.location.reload();

  }
  increase(item: CartItem) {
    let itemId;
    itemId = this.cartService.basket.findIndex(({ productId }) => productId == item.productId);



    item.quantity = item.quantity + 1;
    this.basket[itemId].quantity = item.quantity;


    this.cartService.saveBasket4(this.basket);
    this.cartProducts = this.cartService.getBasket();
    this.grandTotal = this.cartService.getTotalPrice();




  }
  decrease(item: CartItem) {
    if (item.quantity > 1) {

      let itemId;
      itemId = this.cartService.basket.findIndex(({ productId }) => productId == item.productId);



      item.quantity = item.quantity - 1;
      this.basket[itemId].quantity = item.quantity;


      this.cartService.saveBasket4(this.basket);
      this.cartProducts = this.cartService.getBasket();
      this.grandTotal = this.cartService.getTotalPrice();

    }
    else {
      alert("Quantity can not be negative.")
    }
    this.cartService.saveBasket();
  }



}
