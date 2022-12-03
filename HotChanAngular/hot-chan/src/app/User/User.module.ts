import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './User.component';
import { UserRoutes } from './user.routing';
import { ReactiveFormsModule } from '@angular/forms';
import { CreateUserComponent } from './CreateUser/CreateUser.component';

@NgModule({
  imports: [
    CommonModule,
    UserRoutes,
    ReactiveFormsModule
  ],
  declarations: [UserComponent, CreateUserComponent]
})
export class UserModule { }
