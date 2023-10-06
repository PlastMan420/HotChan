import { Routes, RouterModule, CanActivateFn } from '@angular/router';
import { CreateUserComponent } from './CreateUser/CreateUser.component';
import { UserComponent } from './User.component';
import { LoginUserComponent } from './LoginUser/LoginUser.component';
import { inject } from '@angular/core';
import { AuthenticationService } from '../shared/services/Authentication.service';

const routes: Routes = [
  { path: 'new', component: CreateUserComponent },
  { path: 'login', component: LoginUserComponent },
  { path: 'details', component: UserComponent, canActivate: [authenticationGuard] }
];

export const UserRoutes = RouterModule.forChild(routes);

export function authenticationGuard(): CanActivateFn {
  return () => {    
    const authService = inject(AuthenticationService);
  
    return authService.isAuthenticated;
  };
}
