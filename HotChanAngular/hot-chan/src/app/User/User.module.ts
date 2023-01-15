import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './User.component';
import { UserRoutes } from './user.routing';
import { ReactiveFormsModule } from '@angular/forms';
import { CreateUserComponent } from './CreateUser/CreateUser.component';
import { UserProfileDetailsComponent } from './user-profile-details/user-profile-details.component';
import { DataService } from '../Internet/Data.service';

@NgModule({
  imports: [CommonModule, UserRoutes, ReactiveFormsModule],
  declarations: [
    UserComponent,
    CreateUserComponent,
    UserProfileDetailsComponent,
  ],
  providers: [DataService],
})
export class UserModule {}
