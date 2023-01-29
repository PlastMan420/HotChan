import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { FooterFunction } from '../../types/FooterFunction';

export interface FooterFunctionsState {
    functions: FooterFunction[];
}

export const initState: FooterFunctionsState = {
    functions: [],
};

@Injectable()
export class FooterState extends ComponentStore<FooterFunctionsState> {
    constructor() {
        super(initState);
    }

    functions$ = this.select(s => s.functions);

    appendFooterFunction(func: FooterFunction) {
        this.setState((s) => {
            return {
                ...s,
                functions: [...s.functions, func],
            };
        });
    }

    clearFooter = this.updater((s: FooterFunctionsState) => {
        return {
            ...s,
            functions: [],
        };
    });

    updateElement = this.updater(
        (s: FooterFunctionsState, prop: FooterFunction) => {
            return {
                ...s,
                functions: s.functions.map((x) => {
                    if (x.label === prop.label) {
                        return prop;
                    }
                    return x;
                }),
            };
        }
    );
}
