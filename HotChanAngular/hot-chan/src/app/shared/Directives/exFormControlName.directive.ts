import { Directive, forwardRef, Inject, InjectionToken, Optional, Self } from '@angular/core';
import { AsyncValidator, AsyncValidatorFn, ControlContainer, ControlValueAccessor, FormControl, FormControlName, NgControl, NG_ASYNC_VALIDATORS, NG_VALIDATORS, NG_VALUE_ACCESSOR, Validator, ValidatorFn } from '@angular/forms';

export const NG_MODEL_WITH_FORM_CONTROL_WARNING =
    new InjectionToken('NgModelWithFormControlWarning');
    
@Directive({
    // eslint-disable-next-line @angular-eslint/directive-selector
    selector: '[exFormControlName]',
    providers: [
        {
            provide: NgControl, //<-- NgControl is the key
            useExisting: forwardRef(() => ExFormControlNameDirective),
        },
    ],
})
export class ExFormControlNameDirective extends FormControlName {
    constructor(
        @Optional()
        @Self()
        @Inject(NG_VALIDATORS)
        validators: Array<Validator | ValidatorFn>,
        @Optional()
        @Self()
        @Inject(NG_ASYNC_VALIDATORS)
        asyncValidators: Array<AsyncValidator | AsyncValidatorFn>,
        @Optional()
        @Self()
        @Inject(NG_VALUE_ACCESSOR)
        valueAccessors: ControlValueAccessor[],
        parent: ControlContainer,
        @Optional()
        @Inject(NG_MODEL_WITH_FORM_CONTROL_WARNING)
        _ngModelWarningConfig: string | null
    ) {
        super(parent, validators, asyncValidators, valueAccessors, _ngModelWarningConfig);
        //this.form = new FormControl();
    }
}
