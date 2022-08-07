import { Routes, RouterModule } from '@angular/router';
import { CatalogComponent } from './catalog/catalog.component';
import { PostUploadComponent } from './post-upload/post-upload.component';
import { PostViewComponent } from './post-view/post-view.component';

const routes: Routes = [
  { path: 'view/:postid', component: PostViewComponent },
	{ path: 'upload', component: PostUploadComponent },
  { path: '', component: CatalogComponent },
];

export const PostRoutes = RouterModule.forChild(routes);
