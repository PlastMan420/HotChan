import { Component, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { firstValueFrom, Observable, takeUntil } from 'rxjs';
import { Post } from 'src/app/Internet/Types';
import { AbstractComponent } from 'src/app/shared/abstract/AbstractComponent';
import { FooterService } from 'src/app/shared/footer/footer.service';

const CATALOG = gql`
query HotChanQuery {
  postCatalog{
    postId,
    postTitle,
    description,
    thumbnailUrl,
    createdOn
  }
}
`;

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent extends AbstractComponent implements OnInit {

  constructor(private apollo: Apollo, footerSrv: FooterService) { 
    super(footerSrv);
  }
  catalog!: Post[];
  
  ngOnInit() {
    this.initFooter([
      // {
      //     label: 'votes',
      //     type: 'label',
      //     dataStream: this.postScore
      // },
      {
          label: 'Refresh',
          func: this.getCatalog,
          funcName: this.getCatalog.name,
          funcParams: [1],
          type: 'button',
      },
  ]);

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
