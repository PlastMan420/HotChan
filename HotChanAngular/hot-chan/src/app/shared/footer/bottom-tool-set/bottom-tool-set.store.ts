import { Injectable } from "@angular/core";
import { ComponentStore } from "@ngrx/component-store";
import { FooterFunction } from "../../types/FooterFunction";

export interface FooterFunctionsState {
	functions: FooterFunction[];
}

export const initState: FooterFunctionsState = {
	functions: []
};

@Injectable()
export class FooterState extends ComponentStore<FooterFunctionsState> {
	constructor(){
		super(initState);
	}

	appendFooterFunction(func: FooterFunction){
		this.setState(s => {
			return {
				...s,
				functions: [...s.functions, func]
			};
		});
	}
}