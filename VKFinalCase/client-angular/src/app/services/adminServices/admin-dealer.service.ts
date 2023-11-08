import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AdminDealerService {
  private apiUrl = 'http://localhost:5298';
  constructor(
    private http: HttpClient
  ) {}
  getDealers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Dealers`);
  }

  getDealer(DealerId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Dealers/${DealerId}`);
  }

  postDealer(data:any): Observable<any> {
    return this.http.post(`${this.apiUrl}/vk/api/v1/Dealers`,data);
  }
  putDealer(DealerId: number,data:any): Observable<any> {
    return this.http.put(`${this.apiUrl}/vk/api/v1/Dealers/${DealerId}`,data);
  }

  deleteDealer(DealerId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/vk/api/v1/Dealers/${DealerId}`);
  }
}
