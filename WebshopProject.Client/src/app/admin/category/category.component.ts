import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/_models/category';
import { Product } from 'src/app/_models/product';
import { CategoryService } from 'src/app/_services/category.service';
import { ProductService } from 'src/app/_services/product.service';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  categories: Category[]=[];
  category: Category = {id: 0, categoryName :""};
  message='';
  private sub: any;
  categoryId:number=0;
  products: Product[]=[];

  constructor(private categoryService: CategoryService, private route:ActivatedRoute,private productService:ProductService ) { }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe(x => this.categories = x);
  }

  edit(category: Category):void{
    this.category= category;
  }
  delete(category: Category): void {
    if(confirm("Are you sure you want to delete?")){
      this.categoryService.deleteCategory(category.id)
      .subscribe(()=>{
        this.categories =this.categories.filter (x =>x.id !=category.id);
      });
    }
  }

  save():void{
    this.message='';

    if (this.category.id==0){
      this.categoryService.addCategory(this.category)
      .subscribe({
        next: (x: Category) => {
          this.categories.push(x);
          this.category ={ id: 0, categoryName:""};
          this.message='';

        }, error: (err) => {
          console.log(err.error);
          this.message =Object.values(err.error.errors).join(",");
        }
      });
    }
    else{
      this.categoryService.editCategory(this.category.id, this.category)
      .subscribe({
        error:(err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(",");
          this.message='';
        },
        complete: () =>{
          this.category = {id: 0, categoryName :""};

        }
      })
    }
  }
  cancel():void{

    this.category ={id: 0, categoryName :""};

  }
}
