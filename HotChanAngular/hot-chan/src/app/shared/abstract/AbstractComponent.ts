import { FooterService } from '../footer/footer.service';
import { Footer, FooterFunction } from '../types/FooterFunction';

export abstract class AbstractComponent {
    constructor(public footerSrv: FooterService) {
        this.footerSrv.callFn.subscribe({
            next: (x) => {
                const funcName = x.pageFunctions[0].funcName;
                const params = x.pageFunctions[0].funcParams as [] ?? [];
                const ctx = x.context;

                const f = ctx[funcName as keyof typeof ctx] as Function;

                // use reflection
                Reflect.apply(f, undefined, [ctx, ...params]);
            },
        });
    }

    // all abstract methods must have the first parameter to be the execution context.

    public initFooter(ctx: this, functions: FooterFunction[]) 
    {
        const footer = {
            context: ctx,
            footerLabel: '',
            pageFunctions: functions,
        } as Footer;

        this.footerSrv.setFooter(footer);
    }

    public set FooterLabel(footerLabel: string) {
        this.footerSrv.footerLabel = footerLabel;
    }

    public get FooterLabel() {
        return this.footerSrv.footerLabel;
    }

    public setFooterLabel(footerLabel: string, footerIcon?: string) {
        this.footerSrv.setfooterLabel(footerLabel, footerIcon);
    }
}
