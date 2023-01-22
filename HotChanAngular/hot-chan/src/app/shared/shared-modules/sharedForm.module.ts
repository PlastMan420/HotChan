import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {InputTextModule} from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {PasswordModule} from 'primeng/password';
import {MultiSelectModule} from 'primeng/multiselect';
import {RatingModule} from 'primeng/rating';
import {SelectButtonModule} from 'primeng/selectbutton';
import {EditorModule} from 'primeng/editor';
import {InputSwitchModule} from 'primeng/inputswitch';
import {DropdownModule} from 'primeng/dropdown';
import {CheckboxModule} from 'primeng/checkbox';
import {ChipsModule} from 'primeng/chips';
import {ButtonModule} from 'primeng/button';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [],
  exports: [
    ReactiveFormsModule,
    FormsModule,
		InputTextareaModule,
		InputTextModule,
    PasswordModule,
    ChipsModule,
    CheckboxModule,
    DropdownModule,
    InputSwitchModule,
    EditorModule,
    SelectButtonModule,
    RatingModule,
    MultiSelectModule,
    ButtonModule
  ]
})
export class SharedFormModule { }
