import {Component, OnInit} from '@angular/core';
import {initFlowbite} from "flowbite";
import {provideRouter} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'client';

  constructor() {}

  ngOnInit(): void {
    initFlowbite();

  }

  protected readonly provideRouter = provideRouter;
  protected readonly window = window;
}
