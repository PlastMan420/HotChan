import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';
import saveAs from 'file-saver';
import { firstValueFrom, Subject, Subscription, takeUntil } from 'rxjs';
import { Post } from 'src/app/Internet/Types';
import { AbstractComponent } from 'src/app/shared/abstract/AbstractComponent';
import { FooterService } from 'src/app/shared/footer/footer.service';

const GET_POST = gql`
    query HotChanQuery($postId: UUID!) {
        post(where: {postId: {eq: $postId}}) {
            nodes {
                postTitle
                description
                mediaUrl
                createdOn
                postId
                score
            }
        }
    }
`;

const UPVOTE = gql`
    mutation HotChanMutation($postId: UUID!, $score: Int!) {
        togglePostScore(postId: $postId, score: $score)
    }
`;

@Component({
    selector: 'app-post-view',
    templateUrl: './post-view.component.html',
    styleUrls: ['./post-view.component.scss'],
})
export class PostViewComponent
    extends AbstractComponent
    implements OnInit, OnDestroy
{
    private querySubscription: Subscription = new Subscription();

    loading = true;
    post!: Post;
    postId: string | null = null;
    postScore: Subject<string> = new Subject<string>();

    constructor(
        private route: ActivatedRoute,
        private apollo: Apollo,
        footerSrv: FooterService
    ) {
        super(footerSrv);
    }

    ngOnDestroy() {
        this.footerSrv.clearFooter();
        this.querySubscription.unsubscribe();
        this.destroy$.next(true);
        this.destroy$.complete();
    }

    ngOnInit() {
        this.route.paramMap.subscribe({
            next: (params) => {
                this.postId = params.get('postid');
            },
        });

        if (this.postId) {
            this.getPostGql(this.postId);
        }

        this.initFooter([
            {
                func: this.toggleVote,
                funcName: this.toggleVote.name,
                funcParams: [-1],
                type: 'button',
                buttonIconClass: 'ico downvote',
                class: 'p-button-rounded'
            },
            {
                func: this.toggleVote,
                funcName: this.toggleVote.name,
                funcParams: [1],
                type: 'button',
                buttonIconClass: 'ico upvote',
                class: 'p-button-rounded'
            },
            {
                label: 'votes',
                type: 'label',
                dataStream: this.postScore
            },
        ]);
    }

    async toggleVote(score: number) {
        let post = this.post;

        this.apollo
            .mutate({
                mutation: UPVOTE,
                variables: {
                    postId: post.postId,
                    score: score,
                },
            })
            .subscribe({
                next: (data: any) => {
                    this.postScore.next(data.data.togglePostScore);
                },
                error: (error: unknown) => {
                    console.error('bloopers', error);
                },
            });
    }

    download() {
        saveAs(this.post.mediaUrl, this.post.postTitle);
    }

    async getPostGql(postId: string) {
        const source$ = this.apollo
            .watchQuery<any>({
                query: GET_POST,
                variables: {
                    postId: postId,
                },
            })
            .valueChanges.pipe(takeUntil(this.destroy$));

        try {
            const response = await firstValueFrom(source$);

            this.post = response.data.post.nodes[0];
            this.FooterLabel = `<strong>Post:</strong> ${this.post.postTitle}`;
            this.postScore.next(this.post.score.toString());
            this.loading = false;
        } catch (e) {
            console.error(e);
        }
    }
}
