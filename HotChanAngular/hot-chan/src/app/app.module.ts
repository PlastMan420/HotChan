import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module';

import { InMemoryCache } from '@apollo/client';
import { ApolloModule, APOLLO_OPTIONS} from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { environment } from 'src/environments/environment';
import { GraphQLModule } from './graphql.module';
import { FooterModule } from './shared/footer/footer.module';
import { CommonModule } from '@angular/common';

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
		SharedModule,
		GraphQLModule,
		FooterModule,
		CommonModule
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
