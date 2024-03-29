import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../Internet/Data.service';
import { PostViewComponent } from './post-view/post-view.component';
import { CatalogComponent } from './catalog/catalog.component';
import { SharedModule } from '../shared/shared.module';
import { GalleryModule } from 'ng-gallery';
import { PostRoutes } from './post.routing';
import { PostUploadComponent } from './post-upload/post-upload.component';
import { JournalPostCreateComponent } from './journal-post-create/journal-post-create.component';
import { SharedFormModule } from '../shared/shared-modules/sharedForm.module';
import {CardModule} from 'primeng/card';
import { CommentsSectionModule } from './comments-section/comments-section.module';

@NgModule({
  imports: [
    CommonModule,
    PostRoutes,
    SharedModule,
    GalleryModule,
    SharedFormModule,
    CardModule,
    CommentsSectionModule
  ],
  declarations: [PostViewComponent, CatalogComponent, PostUploadComponent, JournalPostCreateComponent],
  providers: [DataService],
})
export class PostsModule {}
