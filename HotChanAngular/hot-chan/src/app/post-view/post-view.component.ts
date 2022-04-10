import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';
import { Subscription } from 'rxjs';

const GET_POSTS = gql`
	query GetPostsOfAuthor($authorId: Int!) {
			postsOf(authorId: $authorId) {
				id
				title
		}
	}
`;

@Component({
	selector: 'app-post-view',
	templateUrl: './post-view.component.html',
	styleUrls: ['./post-view.component.scss']
})
export class PostViewComponent implements OnInit, OnDestroy  {

	private querySubscription: Subscription = new Subscription;

	loading: boolean = false;
	posts: any;
	postId: string | null = null;

	postForm = this.fb.group({
		postTitle: [null],
		postDescription: [null],

	});

	constructor(private fb: FormBuilder, private apollo: Apollo, private route: ActivatedRoute) { }

	ngOnInit() {
		this.route.paramMap.subscribe(params => {
			this.postId = params.get('postid');
		});

		if(!!this.postId){
			this.querySubscription = this.apollo.watchQuery<any>({
				query: GET_POSTS,
				variables: {
          postId: this.postId,
        },
			})
			.valueChanges
			.subscribe(({ data, loading }) => {
				this.loading = loading;
				this.posts = data.posts;
			});
		}
	}

	ngOnDestroy() {
		this.querySubscription.unsubscribe();
	}

}
