import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-navbar',
	templateUrl: './navbar.component.html',
	styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

	NavDirs = new Map([
		["Home", "/"],
		["Account", "/account"],
		["Upload", "/upload"],
		["Recent", "/recent"],
		["sample post", "/post"]
	]);

	constructor() { }

	ngOnInit() {
	}

	asIsOrder() {
		return 1;
	}

}
