import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartItem } from '../_models/cartItem';
import { Product } from '../_models/product';
import { CartService } from '../_services/cart.service';
import { ProductService } from '../_services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {


  productId: number =0;
  
  product:Product={id: 0, title:"", price:0, description:"",image:"", stock:0,categoryId:0}
  constructor(private productService:ProductService, private cartService:CartService, private route:ActivatedRoute) { }
 
  public quantity:number=0;
  //@Output() quantity = new EventEmitter<number>();

  ngOnInit(): void {
     this.route.params.subscribe(params => {
      this.productId = +params['id'];
    });
    this.productService.getProductById(this.productId).subscribe(x=> 
      { this.product=x,
      
      console.log(this.product.stock);
    });
    
    
  }

  addToCart(product:Product)
  {
    
    this.cartService.addToBasket({
      productId:product.id,
      productTitle:product.title,
      productPrice:product.price,
      quantity:this.quantity + 1,
     
    });
  // this.cartService.getBasket();
  //    this.cartService.getTotalPrice();
  //     this.cartService.saveBasket();
  window.location.reload();

  }
}
