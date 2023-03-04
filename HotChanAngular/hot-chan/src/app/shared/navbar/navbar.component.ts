import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/Authentication.service';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
    constructor(public authService: AuthenticationService) {}


    NavDirs = new Map([
        ['Home', '/'],
        ['Upload', '/post/upload'],
        ['New Journal', '/post/submitjournal'],
        ['Recent', '/post'],
        ['sample gql post', '/post/view/28e2dfef-f339-4ab6-b389-30afa2844846'],
    ]);

    ngOnInit() {
        this.authService.activeSession$.subscribe({
            next: (x) => {
                if (!x) {
                    this.NavDirs.set('Register', '/user/new');
                }
            },
        });
    }

    asIsOrder() {
        return 1;
    }
}
