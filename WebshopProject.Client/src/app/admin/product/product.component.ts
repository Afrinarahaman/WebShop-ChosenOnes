import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';
import { CategoryComponent } from '../category/category.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: Product[] = [];
  productId: number = 0;


  message: string[] = [];
  product: Product = { id: 0, title: "", price: 0, description: "", image: "", stock: 0, categoryId: 0 }
  constructor(private productService: ProductService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getAllProducts();

  }



  getAllProducts() {
    this.productService.getAllProducts()
      .subscribe(x => this.products = x);
  }

  edit(product: Product): void {
    this.product = product;
    this.message = [];
  }

  delete(product: Product): void {
    console.log("Delete", product)
    if (confirm('Are you sure you want to delete?')) {

      this.productService.deleteProduct(product.id)
        .subscribe(() => {

          this.products = this.products.filter(a => a.id != product.id)
          console.log("subscribe", this.products)
          this.getAllProducts();
        });
    }
  }

  cancel(): void {
    this.message = [];
    this.product = { id: 0, title: "", price: 0, description: "", image: "", stock: 0, categoryId: 0 };

  }

  save(): void {
    this.message = [];

    if (this.product.title == '') {
      this.message.push('Enter title');
    }
    if (this.product.price == 0) {
      this.message.push('Enter price');
    }
    if (this.product.description == '') {
      this.message.push('Enter Description');
    }
    if (this.product.stock == 0) {
      this.message.push('Enter stock');
    }
    if (this.product.image == '') {
      this.message.push('Enter Image');
    }
    if (this.product.categoryId == 0) {
      this.message.push('Enter Category');
    }
    console.log(this.product);

    if (this.message.length == 0) {
      if (this.product.id == 0) {
        this.productService.addProduct(this.product)
          .subscribe(a => {
            this.products.push(a);

            this.product = { id: 0, title: "", price: 0, description: "", image: "", stock: 0, categoryId: 0 };

          });
      } else {
        this.productService.updateProduct(this.product.id, this.product)
          .subscribe(() => {
            this.product = { id: 0, title: "", price: 0, description: "", image: "", stock: 0, categoryId: 0 };
          });
      }
    }
  }





  
}




