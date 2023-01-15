import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from 'src/app/Internet/Data.service';
import { UserAuth } from 'src/app/Internet/Types';

@Component({
  selector: 'app-CreateUser',
  templateUrl: './CreateUser.component.html',
  styleUrls: ['./CreateUser.component.scss']
})
export class CreateUserComponent implements OnInit {

  constructor(private fb: FormBuilder, private dataservice: DataService) { }
  
  userForm!: FormGroup;
  
  ngOnInit() {
    this.initForm();
    
  }

  private initForm(){
    this.userForm = this.fb.group({
      userName: [null, Validators.required],
      emailAddress: [null, [Validators.required, Validators.email]],
      key: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(14)]],
    });
  }

  submit(){
    if(this.userForm.valid){
      const data = this.userForm.value as UserAuth;
      console.log(data);
      this.dataservice.CreateUser(data);
    }
    else{
      console.log("no")
    }
  }
}
