import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCommentsComponent } from './post-comments/post-comments.component';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [PostCommentsComponent, NavbarComponent],
  exports: [
    PostCommentsComponent, NavbarComponent
  ]
})
export class SharedModule { }
