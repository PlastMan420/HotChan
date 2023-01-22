import { Routes, RouterModule } from '@angular/router';
import { CatalogComponent } from './catalog/catalog.component';
import { JournalPostCreateComponent } from './journal-post-create/journal-post-create.component';
import { PostUploadComponent } from './post-upload/post-upload.component';
import { PostViewComponent } from './post-view/post-view.component';

const routes: Routes = [
  { path: 'view/:postid', component: PostViewComponent },
	{ path: 'upload', component: PostUploadComponent },
  { path: 'submitjournal', component: JournalPostCreateComponent },
  { path: '', component: CatalogComponent },
];

export const PostRoutes = RouterModule.forChild(routes);
