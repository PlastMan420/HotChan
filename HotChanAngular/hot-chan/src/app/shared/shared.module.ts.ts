import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCommentsComponent } from './post-comments/post-comments.component';
import { NavbarComponent } from './navbar/navbar.component';
import { BottomToolSetComponent } from './footer/bottom-tool-set/bottom-tool-set.component';
import { ButtonModule } from 'primeng/button';
import {BadgeModule} from 'primeng/badge';

@NgModule({
  imports: [
    CommonModule,
    ButtonModule,
    BadgeModule
  ],
  declarations: [PostCommentsComponent, NavbarComponent, BottomToolSetComponent],
  exports: [
    PostCommentsComponent, NavbarComponent, BottomToolSetComponent
  ]
})
export class SharedModule { }
