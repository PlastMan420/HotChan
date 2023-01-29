import { Component, OnInit } from '@angular/core';
import {
    AbstractControl,
    FormControl,
    FormGroup,
    Validators,
} from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { Post } from 'src/app/Internet/Types';
import { AbstractComponentWithForm } from 'src/app/shared/abstract/AbstractComponentWithForm';
import { FooterService } from 'src/app/shared/footer/footer.service';

const SUBMIT = gql`
    mutation HotChanMutation(
        $postId: UUID!
        $postTitle: String!
        $description: String
        $tags: [String]
        $hidden: Boolean!
        $createdOn: DateTime!
    ) {
        submitJournalPost(
            post: {
                postTitle: $postTitle
                description: $description
                postId: $postId
                tags: $tags
                hidden: $hidden
                createdOn: $createdOn
            }
        ) {
            postId
            postTitle
            description
            createdOn
            hidden
        }
    }
`;

@Component({
    selector: 'app-journal-post-create',
    templateUrl: './journal-post-create.component.html',
    styleUrls: ['./journal-post-create.component.scss'],
})
export class JournalPostCreateComponent
    extends AbstractComponentWithForm
    implements OnInit
{
    override form: FormGroup<any> = new FormGroup({
        postTitle: new FormControl<string>('<Post Title>', [
            Validators.required,
            Validators.minLength(3),
        ]),
        description: new FormControl(null, Validators.required),
        hidden: new FormControl(false),
        tags: new FormControl(['test', 'testing'], Validators.required),
    });

    constructor(footerSrv: FooterService, private apollo: Apollo) {
        super(footerSrv);
    }

    readonly placeholderPostTitle = '<Post Title>';

    ngOnInit() {
        this.initFooterForForm(this);
        this.FooterLabel = 'Submit New Journal';
    }

    override submit(ctx: this) {
        if (!ctx.formIsValidAndSane()) {
            console.error('you fucking nigger');

            return;
        }

        let post = ctx.form.value as Post;
        post.postId = '00000000-0000-0000-0000-000000000000';
        console.log(post);
        post.createdOn = '2023-01-26T19:53:33.485Z';
        console.log('uploading post...');
        ctx.apollo
            .mutate({
                mutation: SUBMIT,
                variables: {
                    postId: post.postId,
                    postTitle: post.postTitle,
                    description: post.description,
                    tags: post.tags,
                    hidden: post.hidden,
                    createdOn: new Date(),
                },
            })
            .subscribe({
                next: ({ data }) => {
                    console.log('Post was submitted successfully', data);
                },
                error: (error: unknown) => {
                    console.log('bloopers', error);
                },
            });
    }

    private formIsValidAndSane(): boolean {
        const postTitleControl = this.form.get('postTitle') as AbstractControl;

        postTitleControl.setErrors({
            'Yeaaaa, this is not a valid post title':
                postTitleControl.value === this.placeholderPostTitle,
        });
        console.log(postTitleControl.errors);

        if (this.form.valid) {
            return true;
        }
        return false;
    }
}
