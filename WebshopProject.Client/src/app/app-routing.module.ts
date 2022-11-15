import { Customer } from './_models/customer';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './admin/category/category.component';
import { ProductComponent } from './admin/product/product.component';
import { CustomerComponent } from './admin/customer/customer.component';
import { CartComponent } from './cart/cart.component';
import { CategoryProductsComponent } from './category-products/category-products.component';
import { LoginComponent } from './login/login.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/customer';
import { ProfileComponent } from './profile/profile.component';
import { ThankyouComponent } from './thankyou/thankyou.component';


const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'admin/category/:id', component: CategoryComponent },
  {path: 'admin/product', component: ProductComponent},

  //{ path: 'admin/product/:id', component: ProductComponent },
  { path: 'product_detail/:id', component: ProductDetailComponent },
  { path: 'category-products/:id', component: CategoryProductsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'thankyou/:id', component: ThankyouComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard], data: { roles: [Role.Customer, Role.Admin] } },
  { path: 'admin/customer', component: CustomerComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
  { path: 'admin/category', component: CategoryComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
  { path: 'admin/product', component: ProductComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
