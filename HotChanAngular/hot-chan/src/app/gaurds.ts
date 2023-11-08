import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from './shared/services/Authentication.service';
import { inject } from '@angular/core';

export function authenticationGuard(): CanActivateFn {
    return () => {
        const authService = inject(AuthenticationService);
        const router = inject(Router);

        if (authService.isAuthenticated) {
            return authService.isAuthenticated;
        } else {
            router.navigateByUrl('/user/login');
            return false;
        }
    };
}
