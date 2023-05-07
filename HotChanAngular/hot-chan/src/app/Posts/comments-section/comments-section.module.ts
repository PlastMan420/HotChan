import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentsListComponent } from './comments-list/comments-list.component';
import { SubmitCommentComponent } from './submit-comment/submit-comment.component';
import { SharedFormModule } from 'src/app/shared/shared-modules/sharedForm.module';

@NgModule({
  imports: [
    CommonModule,
    SharedFormModule
  ],
  declarations: [CommentsListComponent, SubmitCommentComponent],
  exports: [SubmitCommentComponent, CommentsListComponent]
})
export class CommentsSectionModule { }
