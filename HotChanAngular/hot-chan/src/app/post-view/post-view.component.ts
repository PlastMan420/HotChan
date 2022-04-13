import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';
import { firstValueFrom, lastValueFrom, Subject, Subscription, takeUntil } from 'rxjs';

type Post = {
	postTitle: string,
	description: string,
	mediaUrl: string,
	time: Date,
};

const GET_POSTS = gql`
	query GetPostById($postId: UUID!){
		post(PostId: $postId) {
			postTitle
			description
			mediaUrl
			time
		}
	}
`;

@Component({
	selector: 'app-post-view',
	templateUrl: './post-view.component.html',
	styleUrls: ['./post-view.component.scss'],
})
export class PostViewComponent implements OnInit, OnDestroy {
	private querySubscription: Subscription = new Subscription();
	private destroy$: Subject<boolean> = new Subject<boolean>();

	loading: boolean = false;
	post!: Post;
	postId: string | null = null;
	
	constructor(
		private apollo: Apollo,
		private route: ActivatedRoute
	) {}

	ngOnDestroy() {
		this.querySubscription.unsubscribe();
		this.destroy$.next(true);
		this.destroy$.unsubscribe();
	}

	ngOnInit() {
		this.route.paramMap.subscribe((params) => {
			this.postId = params.get('postid');
		});

		if (!!this.postId) {
			console.log(this.postId);
			this.getPost(this.postId);
		}
	}

	async getPost(postId: string) {

		let source$ = this.apollo
			.watchQuery<any>({
				query: GET_POSTS,
				variables: {
					postId: postId,
				},
			})
			.valueChanges
			.pipe(takeUntil(this.destroy$));
      // .subscribe(({ data, loading }) => {
      //   console.log(data.post);
			// 	this.post = data.post
      // });

		try{
			const response = await firstValueFrom(source$);
			console.log(response);
			this.post = response.data.post;
		}
		catch(e){
			console.log(e);
		}

	}
}
