import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-bottom-login',
  templateUrl: './bottom-login.component.html',
  styleUrls: ['./bottom-login.component.scss']
})
export class BottomLoginComponent implements OnInit {
  form!: FormGroup<any>;

  constructor() {
  }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.form = new FormGroup({
        userName: new FormControl<string|null>(null, Validators.required),
        key: new FormControl<string|null>(null, [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(14),
        ]),
    });
}

  submit(ctx: unknown): unknown {
    throw new Error('Method not implemented.');
  }
}
