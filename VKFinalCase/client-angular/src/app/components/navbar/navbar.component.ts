import {Component, OnInit} from '@angular/core'
import {AuthService} from "../../services/auth.service";
import {StorageService} from "../../services/storage.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],


})
export class NavbarComponent implements OnInit {
  isDarkMode = false;
  isLoggin: boolean = false;
  dealerInformations:any;

  constructor(private authService: AuthService,private storage:StorageService) {}

  ngOnInit() {
    const storedDarkMode = localStorage.getItem('darkMode');
    this.isDarkMode = storedDarkMode === 'true';
    this.applyDarkMode(this.isDarkMode);
    this.isLoggin = this.authService.isLoggin();
    this.dealerInformations=this.storage.getUser()
  }

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
    this.applyDarkMode(this.isDarkMode);
    localStorage.setItem('darkMode', this.isDarkMode.toString());
  }

  logout(){
    this.authService.logout();
  }

private applyDarkMode(isDarkMode: boolean) {
    document.body.classList.toggle('dark', isDarkMode);
  }
}
