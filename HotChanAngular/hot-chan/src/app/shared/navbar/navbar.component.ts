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
		["Upload", "/post/upload"],
		["New Journal", "/post/submitjournal"],
		["Recent", "/post"],
		["sample gql post", "/post/view/28e2dfef-f339-4ab6-b389-30afa2844846"],
		["Register", "/user/new"]
	]);

	constructor() { }

	ngOnInit() {
	}

	asIsOrder() {
		return 1;
	}

}
