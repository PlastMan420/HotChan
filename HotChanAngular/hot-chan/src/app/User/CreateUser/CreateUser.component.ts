import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DataService } from 'src/app/Internet/Data.service';
import { AbstractComponent } from 'src/app/shared/abstract/AbstractComponent';
import { FooterService } from 'src/app/shared/footer/footer.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './CreateUser.component.html',
  styleUrls: ['./CreateUser.component.scss']
})
export class CreateUserComponent extends AbstractComponent implements OnInit {

  constructor(footerSrv: FooterService, private dataservice: DataService)
  {
    super(footerSrv)
  }
  
  override readonly formLabel = "User Authentication";

  ngOnInit() {
    this.initForm();
    this.initFooter();
  }

  private initForm(){
    this.form = new FormGroup({
      userName: new FormControl(null, Validators.required),
      emailAddress: new FormControl(null, [Validators.required, Validators.email]),
      key: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(14)]),
    });
  }

  submit(): void {
    alert("booba")
  }
}
