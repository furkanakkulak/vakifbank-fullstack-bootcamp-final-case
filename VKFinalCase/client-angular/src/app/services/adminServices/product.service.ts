import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {PostOrder} from "../../Models/order.model";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5298';
  constructor(
    private http: HttpClient
  ) {}
  getProducts(): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Products`);
  }

  getProduct(productId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Products/${productId}`);
  }

  postProduct(data:any): Observable<any> {
    return this.http.post(`${this.apiUrl}/vk/api/v1/Products`,data);
  }
  putProduct(productId: number,data:any): Observable<any> {
    return this.http.put(`${this.apiUrl}/vk/api/v1/Products/${productId}`,data);
  }

  deleteProduct(productId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/vk/api/v1/Products/${productId}`);
  }
}
