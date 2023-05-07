import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { AbstractComponentWithForm } from 'src/app/shared/abstract/AbstractComponentWithForm';
import { FooterService } from 'src/app/shared/footer/footer.service';
import { AuthenticationService } from 'src/app/shared/services/Authentication.service';

const SUBMIT_Comment = gql`
    mutation HotChanMutation(
        $postId: UUID!
        $userId: UUID!
        $threadId: UUID!
        $commentText: string
    ) {
        addComment(
            postId: $postId
            userId: $userId
            comment: { commentText: $commentText, thread: { id: $threadId } }
        )
    }
`;

@Component({
    selector: 'app-submit-comment',
    templateUrl: './submit-comment.component.html',
    styleUrls: ['./submit-comment.component.scss'],
})
export class SubmitCommentComponent extends AbstractComponentWithForm implements OnInit {
  constructor(footerSrv: FooterService, authSrc: AuthenticationService, private apollo: Apollo) {
    super(footerSrv);
}
    ngOnInit() {}

    override form: FormGroup<any> = new FormGroup({
      postTitle: new FormControl<string | null>(null, [
          Validators.minLength(3),
          this.requiredNoWhiteSpace,
      ]),
  });

    submit(): unknown {
      throw new Error('Method not implemented.');
    }
}
