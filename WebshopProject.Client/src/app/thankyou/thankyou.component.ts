import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartItem } from '../_models/cartItem';
import { Order } from '../_models/order';
import { AuthService } from '../_services/auth.service';
import { OrderService } from '../_services/order.service';

@Component({
  selector: 'app-thankyou',
  templateUrl: './thankyou.component.html',
  styleUrls: ['./thankyou.component.css']
})
export class ThankyouComponent implements OnInit {
  public result:any;
  constructor(private orderService:OrderService, private authService:AuthService, private route:ActivatedRoute) { }
  isShown: boolean = false ;
  public order: Order={id:0,customerId:0,orderDetails:[]} ;
  public orderDetails: Array<CartItem> = [];
  //customerId:number=this.authService.currentCustomerValue.id  ;
  orderId:number=0; 
  ngOnInit(): void {
    
  }
  
   detail(){

    this.orderId = parseInt(this.route.snapshot.paramMap.get('id')||'0');
   
    this.orderService.getOrderDetailsByOrderId(this.orderId).subscribe(res => {
      this.order = res;

      console.log('orderDetails',this.order.orderDetails);
      
      if(res.customerId==this.authService.currentCustomerValue.id)
      {
        this.isShown = ! this.isShown;
      }
     

    // if (this.productList.id) window.localStorage.setItem('orderId', this.result.order_id.toString());
    
    // this.isShown = ! this.isShown;
  });
    
  }

}
