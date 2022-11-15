import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryComponent } from './admin/category/category.component';
import { ProductComponent } from './admin/product/product.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomerComponent } from './admin/customer/customer.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { CategoryProductsComponent } from './category-products/category-products.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
// autoinject JWT into all requests
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { FooterComponent } from './footer/footer.component';
import { ThankyouComponent } from './thankyou/thankyou.component';
import { FilterPipe } from './_shared/filter.pipe';



@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    ProductComponent,
    FrontpageComponent,
    CustomerComponent,

    ProductDetailComponent,
    CategoryProductsComponent,
    CartComponent,


    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    FooterComponent,
    ThankyouComponent,
    FilterPipe

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    //ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
