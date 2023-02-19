import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { takeUntil, firstValueFrom, Subject } from 'rxjs';
import { User, UserLoginDtoInput } from 'src/app/Internet/Types';
import { AbstractComponentWithForm } from 'src/app/shared/abstract/AbstractComponentWithForm';
import { FooterService } from 'src/app/shared/footer/footer.service';

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
    selector: 'app-LoginUser',
    templateUrl: './LoginUser.component.html',
    styleUrls: ['./LoginUser.component.scss'],
})
export class LoginUserComponent
    extends AbstractComponentWithForm
    implements OnInit
{
    override form: FormGroup<any> = new FormGroup({
        userName: new FormControl<string | null>(null, Validators.required),
        key: new FormControl<string | null>(null, [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(14),
        ]),
    });

    constructor(footerSrv: FooterService, private apollo: Apollo) {
        super(footerSrv);
    }

    private destroy$: Subject<boolean> = new Subject<boolean>();

    ngOnInit() {}

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
