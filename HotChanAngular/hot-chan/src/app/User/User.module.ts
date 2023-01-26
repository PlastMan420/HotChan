import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './User.component';
import { UserRoutes } from './user.routing';
import { CreateUserComponent } from './CreateUser/CreateUser.component';
import { UserProfileDetailsComponent } from './user-profile-details/user-profile-details.component';
import { DataService } from '../Internet/Data.service';
import { SharedFormModule } from '../shared/shared-modules/sharedForm.module';

@NgModule({
  imports: [CommonModule, UserRoutes, SharedFormModule],
  declarations: [
    UserComponent,
    CreateUserComponent,
    UserProfileDetailsComponent,
  ],
  providers: [DataService],
})
export class UserModule {}
