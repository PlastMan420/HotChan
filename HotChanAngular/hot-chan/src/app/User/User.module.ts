import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './User.component';
import { UserRoutes } from './user.routing';
import { CreateUserComponent } from './CreateUser/CreateUser.component';
import { UserProfileDetailsComponent } from './user-profile-details/user-profile-details.component';
import { DataService } from '../Internet/Data.service';
import { SharedFormModule } from '../shared/shared-modules/sharedForm.module';
import { LoginUserComponent } from './LoginUser/LoginUser.component';
import {OverlayPanelModule} from 'primeng/overlaypanel';

@NgModule({
  imports: [CommonModule, UserRoutes, SharedFormModule, OverlayPanelModule],
  declarations: [
    UserComponent,
    CreateUserComponent,
    UserProfileDetailsComponent,
    LoginUserComponent
  ],
  providers: [DataService],
})
export class UserModule {}
