import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { Post } from 'src/app/Internet/Types';
import { AbstractComponent } from 'src/app/shared/abstract/AbstractComponent';
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
    extends AbstractComponent
    implements OnInit
{
    override form: FormGroup<any> = new FormGroup({
        postTitle: new FormControl(null, [
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
    override readonly formLabel = 'Submit New Journal';

    ngOnInit() {
        this.initFooter();
    }

    override submit(ctx: this) {
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
}
