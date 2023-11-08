import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./views/home/home.component";
import {ProductsComponent} from "./views/products/products.component";
import {ContactComponent} from "./views/contact/contact.component";
import {SupportComponent} from "./views/support/support.component";
import {LoginComponent} from "./views/login/login.component";
import {AuthGuardService} from "./services/auth-guard.service";
import {ProductComponent} from "./views/product/product.component";
import {OrdersComponent} from "./views/orders/orders.component";
import {OrderComponent} from "./views/order/order.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'products', component: ProductsComponent, canActivate: [AuthGuardService] },
  { path: 'products/:id', component: ProductComponent, canActivate: [AuthGuardService] },
  { path: 'orders', component: OrdersComponent, canActivate: [AuthGuardService] },
  { path: 'orders/:id', component: OrderComponent, canActivate: [AuthGuardService] },
  { path: 'contact', component: ContactComponent, canActivate: [AuthGuardService] },
  { path: 'support', component: SupportComponent },
  { path: 'login', component: LoginComponent },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
