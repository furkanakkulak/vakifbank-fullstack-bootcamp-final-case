import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {PostOrder} from "../../Models/order.model";
import {StorageService} from "../storage.service";

@Injectable({
  providedIn: 'root'
})
export class DealerService {
  private apiUrl = 'http://localhost:5298';

  public dealerInformations:any;

  constructor(private http: HttpClient,private storage:StorageService) {

    const role=storage.getUserRole()
    if (role=="dealer") {
      this.getInformations().subscribe(
        (response: any) => {
          if (response.success) {
            this.dealerInformations= response.response
          } else {
            throw new Error(response.message)
          }
        },
        (error: any) => {
          console.error(error);
        }
      );
    }
  }

  getProducts(): Observable<any> {
    return this.http.get(`${this.apiUrl}/products`);
  }

  getProduct(productId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/products/${productId}`);
  }

  postOrder(orderData: PostOrder): Observable<any> {
    return this.http.post(`${this.apiUrl}/orders`, orderData);
  }

  getOrders(): Observable<any> {
    return this.http.get(`${this.apiUrl}/orders`);
  }

  getOrder(orderId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/orders/${orderId}`);
  }

  deleteOrder(orderId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/orders/${orderId}`);
  }

  postOrderPayment(paymentData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/orderPayment`, paymentData);
  }

  getPaymentMethods(): Observable<any> {
    return this.http.get(`${this.apiUrl}/paymentMethods`);
  }

  getInformations(): Observable<any> {
    return this.http.get(`${this.apiUrl}/informations`);
  }
}
