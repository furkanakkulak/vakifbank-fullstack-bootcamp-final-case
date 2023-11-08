import { Component } from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {StorageService} from "../../services/storage.service";
import {ToastrService} from "ngx-toastr";
import {NavbarComponent} from "../../components/navbar/navbar.component";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(
    private authService: AuthService,
    private storage: StorageService,
    private toastr:ToastrService,
  ) {}


  onSubmit() {
    const { username, password } = this.loginForm.value;
    if (username && password) {
      this.authService.login(username, password).subscribe({
        next: (data) => {
          if (data.success) {
            this.storage.saveUser(data.response);
            window.location.href='/'
          } else {
            this.toastr.warning(data.message, 'WARNING');
          }
        },
        error: (err) => {
          this.toastr.error(`${err.status} ${err.statusText}`, 'ERROR');
        },
      });
    }
  }
}
