import { FormGroup } from '@angular/forms';
import { FooterService } from '../footer/footer.service';
import { Footer, FooterFunction } from '../types/FooterFunction';

export abstract class AbstractComponent {
    constructor(private footerSrv: FooterService) {
        this.footerSrv.callFn.subscribe({
            next: (x) => {
                const funcName = x.pageFunctions[0].funcName;
                const ctx = x.context;

                const f = ctx[funcName as keyof typeof ctx] as Function;

                // use reflection
                Reflect.apply(f, undefined, [ctx]);
            },
        });
    }

    abstract form: FormGroup;
    abstract formLabel: string;

    public resetForm(ctx: typeof this) {
        ctx.form.reset();

        for (const field in ctx.form) {
            ctx.form.get(field)?.reset();
        }
    }

    // all abstract methods must have the first parameter to be the execution context.
    abstract submit(ctx: unknown): unknown;

    public initFooter(
        functions: FooterFunction[] = [
            {
                label: 'reset',
                func: this.resetForm,
                funcName: this.resetForm.name,
            },
            { label: 'Submit', func: this.submit, funcName: this.submit.name },
        ]
    ) {
        const footer = {
            context: this,
            footerLabel: this.formLabel,
            pageFunctions: functions,
        } as Footer;

        this.footerSrv.setFooter(footer);
    }
}
