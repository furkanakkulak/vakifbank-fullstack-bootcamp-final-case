import { Injectable } from '@angular/core';
import { Observable} from "rxjs";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {StorageService} from "./storage.service";
import {Router} from "@angular/router";

const BASE_URL = 'http://localhost:5298/vk/api/v1/';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(
    private http: HttpClient,
    private storage:StorageService,
    private router:Router
  ) { }

  login(username: string, password: string): Observable<any> {
    const body = { username, password };
    return this.http.post<any>(`${BASE_URL}Token`, body,httpOptions)
  }

  logout() {
    this.storage.clean();
    this.router.navigate(['login']);
  }
  isLoggin():boolean {
    const user = this.storage.getUser();
    if (user && user.expireDate) {
      const expireDate = new Date(user.expireDate);
      const currentDate = new Date();
      if (expireDate > currentDate) {
        return true
      } else {
        this.logout()
        return false
      }
    }
    return false;
  }
}
