import { FormGroup } from '@angular/forms';
import { FooterService } from '../footer/footer.service';
import { FooterFunction } from '../types/FooterFunction';
import { AbstractComponent } from './AbstractComponent';

export abstract class AbstractComponentWithForm extends AbstractComponent {
    constructor(footerSrv: FooterService) {
        super(footerSrv);
    }

    abstract form: FormGroup;

    public resetForm() {
        this.form.reset();

        for (const field in this.form) {
            this.form.get(field)?.reset();
        }
    }

    // all abstract methods must have the first parameter to be the execution context.
    abstract submit(): unknown;

    public initFooterForForm(
        ctx: this,
        functions: FooterFunction[] = [
            {
                label: 'reset',
                func: this.resetForm,
                funcName: this.resetForm.name,
                type: 'button',
            },
            {
                label: 'Submit',
                func: this.submit,
                funcName: this.submit.name,
                type: 'button',
            },
        ]
    ) {
        this.initFooter(functions);
    }
}
