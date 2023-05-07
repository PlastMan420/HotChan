import { FormGroup, ValidatorFn, Validators } from '@angular/forms';
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

    readonly requiredNoWhiteSpace: ValidatorFn = Validators.compose([
        Validators.required,
        Validators.pattern(/^(?!\s*$)/),
    ]) as ValidatorFn;
    
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

    public showValidation(formControlName: string): boolean
    {
        return this.form.dirty && (!this.form.get(formControlName)?.valid ?? false);
    }
}
