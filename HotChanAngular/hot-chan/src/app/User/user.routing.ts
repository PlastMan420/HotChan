import { Routes, RouterModule } from '@angular/router';
import { CreateUserComponent } from './CreateUser/CreateUser.component';
import { UserComponent } from './User.component';
import {IsLoggedInGuard} from '../shared/guards/isLoggedinGuard';

const routes: Routes = [
  { path: 'new', component: CreateUserComponent },
  { path: 'details', component: UserComponent, canActivate: [IsLoggedInGuard] }
];

export const UserRoutes = RouterModule.forChild(routes);
