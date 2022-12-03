import { Routes, RouterModule } from '@angular/router';
import { CreateUserComponent } from './CreateUser/CreateUser.component';

const routes: Routes = [
  { path: 'user/new', component: CreateUserComponent },
];

export const UserRoutes = RouterModule.forChild(routes);
