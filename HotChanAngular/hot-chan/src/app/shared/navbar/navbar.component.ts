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
		["sample post", "/post"],
		["sample gql post", "/post/4c8c0e04-a727-42b4-bcbc-e24aa082bc07"]
	]);

	constructor() { }

	ngOnInit() {
	}

	asIsOrder() {
		return 1;
	}

}
