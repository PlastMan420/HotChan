import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';
import { takeUntil, firstValueFrom, Subject } from 'rxjs';
import { User, UserLoginDtoInput } from 'src/app/Internet/Types';
import { AbstractComponentWithForm } from 'src/app/shared/abstract/AbstractComponentWithForm';
import { FooterService } from 'src/app/shared/footer/footer.service';
import { AuthenticationService } from 'src/app/shared/services/Authentication.service';

const LOGIN = gql`
    query HotChanQuery($name: String!, $key: String!) {
        login(login: { name: $name, key: $key })
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
    constructor(
        footerSrv: FooterService,
        private apollo: Apollo,
        private authService: AuthenticationService,
        private router: Router
    ) {
        super(footerSrv);

        if(authService.isAuthenticated)
        {
          this.router.navigate(["/"]);
        }
    }
    override form: FormGroup<any> = new FormGroup({
        userName: new FormControl<string | null>(null, Validators.required),
        key: new FormControl<string | null>(null, [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(14),
        ]),
    });

    ngOnInit() {}

    async submit() {
        // if (!this.formIsValidAndSane()) {
        //     console.error('you fucking nigger');

        //     return;
        // }

        const userLogin = this.form.value as UserLoginDtoInput;

        const jwt$ = this.apollo
            .watchQuery({
                query: LOGIN,
                variables: {
                    name: userLogin.userName,
                    key: userLogin.key,
                },
            })
            .valueChanges.pipe(takeUntil(this.destroy$));

        try {
            const response = await firstValueFrom(jwt$);
            console.log(response);
            this.authService.userLogin((<any>response.data).login as string);
        } catch (e) {
            console.error(e);
        }
    }
}
