import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AbstractComponent } from 'src/app/shared/abstract/AbstractComponent';
import { FooterService } from 'src/app/shared/footer/footer.service';

@Component({
    selector: 'app-journal-post-create',
    templateUrl: './journal-post-create.component.html',
    styleUrls: ['./journal-post-create.component.scss']
})
export class JournalPostCreateComponent extends AbstractComponent implements OnInit {
    constructor(footerSrv: FooterService) {
        super(footerSrv)
    }

    readonly placeholderPostTitle = '<Post Title>';
    override readonly formLabel = "Submit New Journal";

    ngOnInit() {
        this.form = new FormGroup({
            title: new FormControl(null, [
                Validators.required,
                Validators.minLength(3),
            ]),
            postText: new FormControl(null, Validators.required),
        });

        this.initFooter();
    }

    submit(): void {
        alert("booba")
      }
}
