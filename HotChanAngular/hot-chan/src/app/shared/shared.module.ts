import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCommentsComponent } from './post-comments/post-comments.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ButtonModule } from 'primeng/button';
import { BadgeModule } from 'primeng/badge';
import { ToastModule } from 'primeng/toast';

@NgModule({
    imports: [CommonModule, ButtonModule, BadgeModule, ToastModule],
    declarations: [PostCommentsComponent, NavbarComponent],
    exports: [PostCommentsComponent, NavbarComponent],
})
export class SharedModule {}
