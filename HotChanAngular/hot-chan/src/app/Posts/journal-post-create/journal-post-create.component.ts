import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FooterService } from 'src/app/shared/footer/footer.service';
import { HotchanService } from 'src/app/shared/hotchan.service';

@Component({
    selector: 'app-journal-post-create',
    templateUrl: './journal-post-create.component.html',
    styleUrls: ['./journal-post-create.component.scss'],
})
export class JournalPostCreateComponent implements OnInit {
    constructor(private footerSrv: FooterService) {}

    readonly placeholderPostTitle = '<Post Title>';

    public journalPostForm = new FormGroup({
        title: new FormControl(null, [
            Validators.required,
            Validators.minLength(3),
        ]),
        postText: new FormControl(null, Validators.required),
    });

    ngOnInit() {
        this.initFooter();

        this.footerSrv.callFn.subscribe((x) => {
            const p = this[x.funcName as keyof typeof this] as Function;
            p(this);
        });
    }

    initFooter() {
        this.footerSrv.setFooter([
            {
                label: 'Reset',
                func: this.resetForm,
                funcName: this.resetForm.name,
            },
            {
                label: 'Submit Post',
                func: this.submit,
                funcName: this.submit.name,
            },
        ]);
    }

    resetForm(ctx: typeof this) {
        ctx.journalPostForm.reset();
        
        for (const field in JournalPostCreateComponent) {
          ctx.journalPostForm.get(field)?.reset();
        }
    }

    submit(ctx: typeof this) {
        this.resetForm(this);
    }
}
