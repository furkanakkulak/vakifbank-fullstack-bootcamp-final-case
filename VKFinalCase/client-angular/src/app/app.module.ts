import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './views/home/home.component';
import { FooterComponent } from './components/footer/footer.component';
import { FaqComponent } from './components/faq/faq.component';
import { TestimonialsComponent } from './components/testimonials/testimonials.component';
import { BenefitsComponent } from './components/benefits/benefits.component';
import { HeroComponent } from './components/hero/hero.component';
import { ProductsComponent } from './views/products/products.component';
import { ContactComponent } from './views/contact/contact.component';
import { SupportComponent } from './views/support/support.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { LoginComponent } from './views/login/login.component';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {httpInterceptorProvider} from "./helpers/token.interceptor";
import {StorageService} from "./services/storage.service";
import { ProductComponent } from './views/product/product.component';
import { OrdersComponent } from './views/orders/orders.component';
import { OrderComponent } from './views/order/order.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    FooterComponent,
    FaqComponent,
    TestimonialsComponent,
    BenefitsComponent,
    HeroComponent,
    ProductsComponent,
    ContactComponent,
    SupportComponent,
    AddProductComponent,
    LoginComponent,
    ProductComponent,
    OrdersComponent,
    OrderComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      progressBar:true,
      closeButton:true,
      tapToDismiss:false
    }),  ],
  providers: [
    StorageService,
    httpInterceptorProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
