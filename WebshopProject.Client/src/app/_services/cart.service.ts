import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, firstValueFrom, Observable } from 'rxjs';
import { CartItem } from '../_models/cartItem';
import { Order } from '../_models/order';
import { AuthService } from './auth.service';
import { OrderService } from './order.service';


@Injectable({
  providedIn: 'root'
})
export class CartService {
  private basketName = "WebShopProjectBasket";
  public basket: CartItem[] = [];
  public search = new BehaviorSubject<string>("");

  constructor(private router: Router, private orderService: OrderService, private authService: AuthService) { }

  getBasket(): CartItem[] {
    this.basket = JSON.parse(localStorage.getItem(this.basketName) || "[]");
    return this.basket;
  }
  saveBasket(): void {
    localStorage.setItem(this.basketName, JSON.stringify(this.basket));
  }
  // saveBasket2(str:string){

  // }
  // tal10:number=5;
  // saveBasket3(tal:number){
  //   this.tal10=tal;

  // }
  saveBasket4(basket: CartItem[]) {
    this.basket = basket;
    this.saveBasket();
  }

  addToBasket(item: CartItem): void {
    this.getBasket();

    let productFound = false;
    // this.basket.forEach(basketItem => {
    // if (basketItem.productId == item.productId) {

    //     basketItem.quantity += item.quantity;
    //     productFound = true;
    //     if (basketItem.quantity <= 0) {
    //       this.removeItemFromBasket(item.productId);
    //      }
    //    }
    // });

    if (!productFound) {
      this.basket.push(item);
    }
    this.saveBasket();
  }


  removeItemFromBasket(productId: number): void {
    this.getBasket();
    for (let i = 0; i < this.basket.length; i += 1) {
      if (this.basket[i].productId === productId) {

        this.basket.splice(i, 1);


      }
    }
    // this.basket.map((a:any, index:any)=>{
    //   if(productId===a.id){
    //     console.log(a.id);
    //     this.basket.splice(index,1)
    //   }
    // })
    this.saveBasket();

  }
  clearBasket(): CartItem[] {
    this.getBasket();
    this.basket = [];
    this.saveBasket();
    return this.basket;
  }
  getTotalPrice(): number {       //This calculates total price of all of the cartitems 
    this.getBasket();
    var grandTotal = 0;
    for (let i = 0; i < this.basket.length; i++) {
      grandTotal += this.basket[i].quantity * this.basket[i].productPrice;
    }
    this.saveBasket();
    return grandTotal;
  }

  async addOrder(): Promise<any> {
    // console.log(localStorage'.getItem("customerId"));


    if (this.authService.currentCustomerValue.id != null && this.authService.currentCustomerValue.id > 0) {



      let orderitem: Order = {           // this is an object which stores customer_id, all of the ordereditems details and date when these have been ordered
        customerId: this.authService.currentCustomerValue.id,
        orderDetails: this.basket,

      }
      var result = await firstValueFrom(this.orderService.storeOrder(orderitem));
      return result;
      //calling storeCartItem function for storing all of the ordereditems deatils into the database. 
      // this.orderService.storeOrder(orderitem);//.subscribe(x => console.log(x));  //calling storeCartItem function for storing all of the ordereditems deatils into the database. 
    } else {
      return null;
    }
    // else {
    //   console.log('null');
    //   return new BehaviorSubject<Order>({ customerId: 0, orderDetails: [] } as Order);

    // }

  }

}


