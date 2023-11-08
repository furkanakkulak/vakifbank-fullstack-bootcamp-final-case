import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private apiUrl = 'http://localhost:5298';
  constructor(
    private http: HttpClient
  ) {}
  getUsers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Users`);
  }

  getUser(UserId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/vk/api/v1/Users/${UserId}`);
  }

  postUser(data:any): Observable<any> {
    return this.http.post(`${this.apiUrl}/vk/api/v1/Users`,data);
  }
  putUser(UserId: number,data:any): Observable<any> {
    return this.http.put(`${this.apiUrl}/vk/api/v1/Users/${UserId}`,data);
  }

  deleteUser(UserId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/vk/api/v1/Users/${UserId}`);
  }
}
