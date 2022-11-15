import { Customer } from '../_models/customer';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../_models/product';
import { ProductService } from '../_services/product.service';
import { CartService } from '../_services/cart.service';
import { FilterPipe } from '../_shared/filter.pipe';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css'],
})
export class FrontpageComponent implements OnInit {
  product: Product = { id: 0, title: "", price: 0, description: "", image: "", stock: 0, categoryId: 0 }
  products: Product[] = [];
  productId: number = 0;
  searchKey: string = "";
  searchProducts: Product[] = [];
  constructor(private productService: ProductService, private cartService: CartService, private route: ActivatedRoute, private router: Router) { }


  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(x =>{ 
      this.products = x;
      this.searchProducts=this.products;
      
    
      this.cartService.search.subscribe(value => {
        
        this.searchKey = value
        console.log(this.searchProducts, this.products);
        this.searchProducts = this.products.filter(x => 
          x.title.toLowerCase().includes(this.searchKey.toLowerCase()) || x.description.toLowerCase().includes(this.searchKey.toLowerCase())
        
      )});
    });

  }



}
