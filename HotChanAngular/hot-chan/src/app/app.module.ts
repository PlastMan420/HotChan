import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module.ts';

import { InMemoryCache } from '@apollo/client';
import { ApolloModule, APOLLO_OPTIONS} from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { environment } from 'src/environments/environment';
import { GraphQLModule } from './graphql.module';

@NgModule({
	declarations: [
		AppComponent,
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		BrowserAnimationsModule,
		ApolloModule,
		HttpClientModule,
		ReactiveFormsModule,
		SharedModule,
		GraphQLModule,
	],
	providers: [
		{
			provide: APOLLO_OPTIONS,
			useFactory: (httpLink: HttpLink) => {
				return {
					cache: new InMemoryCache(),
					link: httpLink.create({
						uri: environment.graphQlServer,
					}),
				};
			},
			deps: [HttpLink],
		}
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
