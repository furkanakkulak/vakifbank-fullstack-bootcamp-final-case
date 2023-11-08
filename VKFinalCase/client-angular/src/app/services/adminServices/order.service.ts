import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'http://localhost:5298';
  constructor(
    private http: HttpClient
  ) {}
  getOrders(): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Orders`);
  }

  getOrder(orderId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Orders/${orderId}`);
  }

  postOrder(data:any): Observable<any> {
    return this.http.post(`${this.apiUrl}/vk/api/v1/Orders`,data);
  }
  putOrder(orderId: number,data:any): Observable<any> {
    return this.http.put(`${this.apiUrl}/vk/api/v1/Orders/${orderId}`,data);
  }

  deleteOrder(orderId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/vk/api/v1/Orders/${orderId}`);
  }

  acceptOrder(orderId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/vk/api/v1/Orders/confirm/${orderId}`,{id:orderId});
  }
}

