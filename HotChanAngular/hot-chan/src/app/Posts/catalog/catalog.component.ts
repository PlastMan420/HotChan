import { Component, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { firstValueFrom, Observable, takeUntil } from 'rxjs';
import { Post } from 'src/app/Internet/Types';

const CATALOG = gql`
query HotChanQuery {
  postCatalog{
    postId,
    postTitle,
    description,
    thumbnailUrl
  }
}
`;

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent implements OnInit {
  destroy$: Observable<any> = new Observable<any>;

  constructor(private apollo: Apollo) { }
  catalog!: Post[];
  
  ngOnInit() {
    this.getCatalog();
  }

  async getCatalog(){
    await this.getPostCatalogGql();
  }

  async getPostCatalogGql() {
    const source$ = this.apollo
      .watchQuery<any>({
        query: CATALOG,
      })
      .valueChanges.pipe(takeUntil(this.destroy$));

    try {
      const response = await firstValueFrom(source$);
      console.log(response);

      this.catalog = response.data.postCatalog;
    } catch (e) {
      console.log(e);
    }
  }
}
