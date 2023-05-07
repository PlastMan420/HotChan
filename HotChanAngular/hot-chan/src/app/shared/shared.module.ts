import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { ButtonModule } from 'primeng/button';
import { BadgeModule } from 'primeng/badge';
import { ToastModule } from 'primeng/toast';

@NgModule({
    imports: [CommonModule, ButtonModule, BadgeModule, ToastModule],
    declarations: [NavbarComponent],
    exports: [NavbarComponent],
})
export class SharedModule {}
