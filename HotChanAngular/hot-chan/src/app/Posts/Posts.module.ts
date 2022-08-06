import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../Internet/Data.service';
import { PostViewComponent } from './post-view/post-view.component';
import { CatalogComponent } from './catalog/catalog.component';
import { SharedModule } from '../shared/shared.module.ts';

@NgModule({
  imports: [
    CommonModule, SharedModule
  ],
  declarations: [PostViewComponent, CatalogComponent],
  providers: [DataService]
})
export class PostsModule { }
