import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  NavDirs: Record<string, string> = {
    "Home": "/",
    "Account": "/account",
    "Upload": "/upload",
    "Recent": "/recent"
  }
  
  constructor() { }

  ngOnInit() {
  }

}
