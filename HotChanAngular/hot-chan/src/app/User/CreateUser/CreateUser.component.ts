import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-CreateUser',
  templateUrl: './CreateUser.component.html',
  styleUrls: ['./CreateUser.component.scss']
})
export class CreateUserComponent implements OnInit {

  constructor(private fb: FormBuilder) { }
  
  userForm!: FormGroup;
  
  ngOnInit() {
  }

  private initForm(){
    this.userForm = this.fb.group({
      userName: [null, Validators.required],
      emailAddress: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(14)]],
    });
  }
}
