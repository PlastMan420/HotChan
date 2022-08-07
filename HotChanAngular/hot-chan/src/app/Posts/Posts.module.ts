import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../Internet/Data.service';
import { PostViewComponent } from './post-view/post-view.component';
import { CatalogComponent } from './catalog/catalog.component';
import { SharedModule } from '../shared/shared.module.ts';
import { GalleryModule } from 'ng-gallery';
import { PostRoutes } from './post.routing';
import { PostUploadComponent } from './post-upload/post-upload.component';

@NgModule({
  imports: [
    CommonModule, PostRoutes, SharedModule, GalleryModule
  ],
  declarations: [PostViewComponent, CatalogComponent, PostUploadComponent],
  providers: [DataService]
})
export class PostsModule { }
