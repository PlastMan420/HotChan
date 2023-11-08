import { Routes, RouterModule } from '@angular/router';
import { CreateUserComponent } from './CreateUser/CreateUser.component';
import { UserComponent } from './User.component';
import { LoginUserComponent } from './LoginUser/LoginUser.component';
import { authenticationGuard } from '../gaurds';

const routes: Routes = [
    { path: 'new', component: CreateUserComponent },
    { path: 'login', component: LoginUserComponent },
    {
        path: 'details',
        component: UserComponent,
        canActivate: [authenticationGuard()],
    },
    { path: '**', redirectTo: 'details' },
];

export const UserRoutes = RouterModule.forChild(routes);
