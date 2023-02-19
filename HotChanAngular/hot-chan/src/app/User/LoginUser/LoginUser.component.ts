import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { User } from 'src/app/Internet/Types';
import { AbstractComponentWithForm } from 'src/app/shared/abstract/AbstractComponentWithForm';
import { FooterService } from 'src/app/shared/footer/footer.service';

const SUBMIT = gql`
    mutation HotChanMutation ($userName: String) {
        register(
            newUser: {
                userName: "testuserxi"
                userMail: "testxi@mairu.com"
                key: "testkey123"
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
    override form!: FormGroup<any>;

    constructor(footerSrv: FooterService, private apollo: Apollo) {
        super(footerSrv);
    }

    ngOnInit() {}

    submit() {
      // if (!this.formIsValidAndSane()) {
      //     console.error('you fucking nigger');

      //     return;
      // }

      let userLogin = this.form.value as User;

      // this.apollo
      //     .mutate({
      //         mutation: SUBMIT,
      //         variables: {
      //             postId: post.postId,

      //         },
      //     })
      //     .subscribe({
      //         next: ({ data }) => {
      //             console.log('Post was submitted successfully', data);
      //         },
      //         error: (error: unknown) => {
      //             console.log('bloopers', error);
      //         },
      //     });
  }
}
