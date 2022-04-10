import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { Subscription } from 'rxjs';

const GET_POSTS = gql`
  query GetPosts {
    posts {
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

  postForm = this.fb.group({
    postTitle: [null],
    postDescription: [null],

  });
  
  constructor(private fb: FormBuilder, private apollo: Apollo) { }

  ngOnInit() {
    this.querySubscription = this.apollo.watchQuery<any>({
      query: GET_POSTS
    })
    .valueChanges
    .subscribe(({ data, loading }) => {
      this.loading = loading;
      this.posts = data.posts;
    });

  }

  ngOnDestroy() {
    this.querySubscription.unsubscribe();
  }

}
