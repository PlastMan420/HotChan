import { HttpHeaders } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { firstValueFrom, takeUntil } from 'rxjs';
import { AbstractComponent } from '../shared/abstract/AbstractComponent';
import { FooterService } from '../shared/footer/footer.service';
import { AuthenticationService } from '../shared/services/Authentication.service';

const GET_USER = gql`
    query HotChanQuery($userId: UUID!) {
      userDetailsPageData(userId: $userId) {
        userName
        email
        registerationDate
      }
    }
`;

@Component({
    selector: 'app-User',
    templateUrl: './User.component.html',
    styleUrls: ['./User.component.scss'],
})
export class UserComponent extends AbstractComponent implements OnInit, OnDestroy {
    constructor(
        footerSrv: FooterService,
        private authSrv: AuthenticationService,
        private apollo: Apollo
    ) {
        super(footerSrv);
    }

    user: any;

    ngOnDestroy() {
      this.destroy$.complete();
    }

    ngOnInit() {
      this.FooterLabel = this.authSrv.userName ?? 'User details';
      console.log(this.authSrv.userId);
      this.getUserDetails();
    }

    async getUserDetails() {
        const source$ = this.apollo
            .watchQuery<any>({
                query: GET_USER,
                variables: {
                    userId: this.authSrv.userId,
                },
                context: {
                  headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("userLogin")),
                }
            })
            .valueChanges.pipe(takeUntil(this.destroy$));

        try {
            const response = await firstValueFrom(source$);
            console.log(response)
            this.user = response.data.userDetailsPageData;

        } catch (e) {
            console.error(e);
        }
    }
}
