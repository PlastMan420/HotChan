import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
// import { InMemoryCache } from '@apollo/client';
// import { ApolloModule, APOLLO_OPTIONS} from 'apollo-angular';
// import { HttpLink } from 'apollo-angular/http';
// import { environment } from 'src/environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { PostsModule } from './Posts/Posts.module';
import { SharedModule } from './shared/shared.module.ts';

@NgModule({
	declarations: [
		AppComponent,
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		//GraphQLModule,
		//ApolloModule,
		HttpClientModule,
		ReactiveFormsModule,
		SharedModule,
		PostsModule
	],
	providers: [
		// {
		// 	provide: APOLLO_OPTIONS,
		// 	useFactory: (httpLink: HttpLink) => {
		// 		return {
		// 			cache: new InMemoryCache(),
		// 			link: httpLink.create({
		// 				uri: environment.graphQlServer,
		// 			}),
		// 		};
		// 	},
		// 	deps: [HttpLink],
		// }
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
