import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {OverlayPanelModule} from 'primeng/overlaypanel';
import { BottomLoginComponent } from './bottom-login/bottom-login.component';
import { BottomToolSetComponent } from './bottom-tool-set/bottom-tool-set.component';
import { SharedFormModule } from '../shared-modules/sharedForm.module';
import { BadgeModule } from 'primeng/badge';

@NgModule({
  imports: [CommonModule, OverlayPanelModule, SharedFormModule, BadgeModule],
  declarations: [
		BottomToolSetComponent,
    BottomLoginComponent
  ],
	exports: [BottomToolSetComponent]
})
export class FooterModule {}
