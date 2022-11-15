import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../_models/product';
import { ProductService } from '../_services/product.service';

@Component({
  selector: 'app-category-products',
  templateUrl: './category-products.component.html',
  styleUrls: ['./category-products.component.css']
})
export class CategoryProductsComponent implements OnInit {

  categoryId:number=0;
  private sub: any;
  products:Product[]=[];
  constructor(private productService:ProductService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.categoryId = +params['id'];
      this.productService.getProductsByCategoryId(this.categoryId).subscribe(x=> this.products=x);
    });
    
    
  }

}
