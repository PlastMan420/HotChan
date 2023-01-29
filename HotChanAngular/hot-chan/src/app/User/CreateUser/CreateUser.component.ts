import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { gql } from 'apollo-angular';
import { DataService } from 'src/app/Internet/Data.service';
import { AbstractComponentWithForm } from 'src/app/shared/abstract/AbstractComponentWithForm';
import { FooterService } from 'src/app/shared/footer/footer.service';

// const USER_REGISTER = gql`
//   mutation HotChanMutation {
//     SubmitJournalPost() {
//       PostDialogueDto: {
        
//       }
//     }
//   }
// `;

@Component({
    selector: 'app-create-user',
    templateUrl: './CreateUser.component.html',
    styleUrls: ['./CreateUser.component.scss'],
})
export class CreateUserComponent
    extends AbstractComponentWithForm
    implements OnInit
{
    constructor(footerSrv: FooterService, private dataservice: DataService) {
        super(footerSrv);
    }

    override form!: FormGroup<any>;

    ngOnInit() {
        this.initForm();
        this.initFooterForForm(this);
        this.FooterLabel = 'User Authentication';
    }

    initForm() {
        this.form = new FormGroup({
            userName: new FormControl<string|null>(null, Validators.required),
            emailAddress: new FormControl<string|null>(null, [
                Validators.required,
                Validators.email,
            ]),
            key: new FormControl<string|null>(null, [
                Validators.required,
                Validators.minLength(6),
                Validators.maxLength(14),
            ]),
        });
    }

    override submit(): void {
        alert('booba');
    }
}
