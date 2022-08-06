import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import saveAs from 'file-saver';
import { Subject, Subscription } from 'rxjs';
import { DataService } from 'src/app/Internet/Data.service';
import { Post } from 'src/app/Internet/Types';

// const GET_POSTS = gql`
// 	query GetPostById($postId: UUID!){
// 		post(PostId: $postId) {
// 			postTitle
// 			description
// 			mediaUrl
// 			time
// 		}
// 	}
// `;

@Component({
	selector: 'app-post-view',
	templateUrl: './post-view.component.html',
	styleUrls: ['./post-view.component.scss'],
})
export class PostViewComponent implements OnInit, OnDestroy {
	private querySubscription: Subscription = new Subscription();
	private destroy$: Subject<boolean> = new Subject<boolean>();

	loading = true;
	post!: Post;
	postId: string | null = null;
	
	constructor(
		private route: ActivatedRoute,
		private data: DataService
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
			this.getPostById(this.postId);
		}
	}

	async getPostById(postId: string)
	{
		this.post = await this.data.getPostById(postId);
		this.loading = false;
	}

	download()
	{
		saveAs(this.post.mediaUrl, this.post.postTitle);
	}
	// async getPostGql(postId: string) {

	// 	let source$ = this.apollo
	// 		.watchQuery<any>({
	// 			query: GET_POSTS,
	// 			variables: {
	// 				postId: postId,
	// 			},
	// 		})
	// 		.valueChanges
	// 		.pipe(takeUntil(this.destroy$));

	// 	try{
	// 		const response = await firstValueFrom(source$);
	// 		console.log(response);
	// 		this.post = response.data.post;
	// 	}
	// 	catch(e){
	// 		console.log(e);
	// 	}
	// }
}
