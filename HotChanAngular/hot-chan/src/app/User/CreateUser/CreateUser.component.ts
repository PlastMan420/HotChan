import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { UserRegisterFormDtoInput } from 'src/app/Internet/Types';
import { AbstractComponentWithForm } from 'src/app/shared/abstract/AbstractComponentWithForm';
import { FooterService } from 'src/app/shared/footer/footer.service';

const USER_REGISTER = gql`
    mutation HotChanMutation(
        $userName: String!
        $userMail: String!
        $key: String!
    ) {
        register(
            newUser: {
                userName: $userName
                userMail: $userMail
                key: $key
            }
        )
    }
`;

@Component({
    selector: 'app-create-user',
    templateUrl: './CreateUser.component.html',
    styleUrls: ['./CreateUser.component.scss'],
})
export class CreateUserComponent
    extends AbstractComponentWithForm
    implements OnInit
{
    constructor(footerSrv: FooterService, private apollo: Apollo) {
        super(footerSrv);
    }

    override form!: FormGroup<any>;

    ngOnInit() {
        this.initForm();
        this.initFooterForForm(this);
        //this.setFooterLabel('User Authentication', 'assets/gifs/lock.gif');
        this.FooterLabel = `<div class="d-flex"><div><img src="assets/gifs/lock.gif"/></div> <div>Register</div></div>`;
    }

    initForm() {
        this.form = new FormGroup({
            userName: new FormControl<string | null>(null, Validators.required),
            userMail: new FormControl<string | null>(null, [
                Validators.required,
                Validators.email,
            ]),
            key: new FormControl<string | null>(null, [
                Validators.required,
                Validators.minLength(6),
                Validators.maxLength(14),
            ]),
        });
    }

    override submit() {
        // if (!ctx.formIsValidAndSane()) {
        //     console.error('you fucking nigger');

        //     return;
        // }

        let user = this.form.value as UserRegisterFormDtoInput;

        this.apollo
            .mutate({
                mutation: USER_REGISTER,
                variables: {
                    userName: user.userName,
                    userMail: user.userMail,
                    key: user.key
                },
            })
            .subscribe({
                next: ({ data }) => {
                    console.log('Created new user successfully', data);
                },
                error: (error: unknown) => {
                    console.log('bloopers', error);
                },
            });
    }

    private formIsValidAndSane() {
        // const postTitleControl = this.form.get('postTitle') as AbstractControl;

        // postTitleControl.setErrors({
        //     'Yeaaaa, this is not a valid post title':
        //         postTitleControl.value === this.placeholderPostTitle,
        // });
        // console.log(postTitleControl.errors);

        // if (this.form.valid) {
        //     return true;
        // }
        // return false;
    }
}
