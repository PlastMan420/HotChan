import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { firstValueFrom, Subject, takeUntil } from 'rxjs';
import { UserLoginDtoInput } from 'src/app/Internet/Types';

const LOGIN = gql`
query HotChanQuery(
    $name: String!
    $key: String!
  ) {
    login(
      login: {
        name: $name
        key: $key
      }
    )
  }
`;


@Component({
  selector: 'app-bottom-login',
  templateUrl: './bottom-login.component.html',
  styleUrls: ['./bottom-login.component.scss']
})
export class BottomLoginComponent implements OnInit, OnDestroy {
  form!: FormGroup<any>;
  private destroy$: Subject<boolean> = new Subject<boolean>();
  
  constructor(private apollo: Apollo) {
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.complete();
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

async submit() {
  // if (!this.formIsValidAndSane()) {
  //     console.error('you fucking nigger');

  //     return;
  // }

  const userLogin = this.form.value as UserLoginDtoInput;
  console.log(userLogin)
  const jwt$ = this.apollo
      .watchQuery({
          query: LOGIN,
          variables: {
            name: userLogin.userName,
            key: userLogin.key
          },
      })
      .valueChanges.pipe(takeUntil(this.destroy$));

      try {
        const response = await firstValueFrom(jwt$);
        console.log(response);

    } catch (e) {
        console.error(e);
    }
  }
}
