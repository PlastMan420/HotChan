import { FormGroup } from '@angular/forms';
import { FooterService } from '../footer/footer.service';
import { Footer, FooterFunction } from '../types/FooterFunction';

export abstract class AbstractComponent {
    constructor(
        private footerSrv: FooterService,
    ) {
        this.footerSrv.callFn.subscribe((x) => {
            const funcName = x.pageFunctions[0].funcName;
            const ctx = x.context;

            const f = ctx[funcName as keyof typeof ctx] as Function;

            // use reflection
            f(ctx);
        });
    }

    form!: FormGroup;
    readonly formLabel!: string;

    public resetForm(ctx: typeof this) {
        ctx.form.reset();

        for (const field in ctx.form) {
            ctx.form.get(field)?.reset();
        }
    }

    abstract submit(): unknown;

    initFooter() {
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
                },
            ] as FooterFunction[],
        } as Footer;

        this.footerSrv.setFooter(footer);
    }
}
