import { Inject, Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FooterService } from './footer/footer.service';
import { Footer, FooterFunction } from './types/FooterFunction';

@Injectable({
    providedIn: 'root',
})
export class FormService {
    
    constructor(private footerSrv: FooterService, @Inject('formLabel') formLabel: string) {
        this.formLabel = formLabel;
        this.initFooter();

        this.footerSrv.callFn.subscribe((x) => {
            const funcName = x.pageFunctions[0].funcName;
            const ctx = x.context;

            const f = ctx[funcName as keyof typeof ctx] as Function;

            // use reflection
            f(ctx);
        });
    }

    form!: FormGroup;
    formLabel!: string;

    public resetForm(ctx: typeof this) {
        ctx.form.reset();

        for (const field in ctx.form) {
            ctx.form.get(field)?.reset();
        }
    }

    submit() {

    }

    private initFooter() {
        const footer = {
            context: this,
            footerLabel: this.formLabel,
            pageFunctions: [
                {
                    label: 'Reset',
                    func: this.resetForm,
                    funcName: this.resetForm.name,
                },
                {
                    label: 'Submit',
                    func: this.submit,
                    funcName: this.submit.name,
                }
            ] as FooterFunction[]
        } as Footer;

        this.footerSrv.setFooter(footer);
    }
}
