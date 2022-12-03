import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
	{
    path: 'post',
    loadChildren: () => import('./Posts/Posts.module').then(m => m.PostsModule)
  },
	{
    path: 'user',
    loadChildren: () => import('./User/User.module').then(m => m.UserModule)
  }
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
