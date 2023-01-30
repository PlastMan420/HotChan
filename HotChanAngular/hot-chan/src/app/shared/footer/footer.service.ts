import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Footer, FooterFunction } from '../types/FooterFunction';

@Injectable({
    providedIn: 'root',
})
export class FooterService {
    constructor() {}

    private _pageFunctions!: FooterFunction[];
    
    pageFooter!: Footer;

    public functionsChangeDetector = new Subject();
    public callFn = new Subject<Footer>();

    callFn$ = this.callFn.asObservable();

    private set pageFunctions(fun: FooterFunction[]) {
        this._pageFunctions = fun;
        this.functionsChangeDetector.next(this._pageFunctions);
    }

    public get pageFunctions(): FooterFunction[] {
        return this._pageFunctions;
    }

    public calledFn(fn: Footer){
        
        this.callFn.next(fn);
    }

    public setFooter(footerFunctions: Footer) {
        this.pageFooter = footerFunctions;
        this.pageFunctions = this.pageFooter.pageFunctions;
    }
  
    public appendFooterFunction(footerFunction: FooterFunction){
        this.pageFooter.pageFunctions.push(footerFunction);
        this.pageFunctions = this.pageFooter.pageFunctions;
    }

    public set footerLabel(footerLabel: string) {
        this.pageFooter.footerLabel = `<i>${footerLabel}</i>`;
    }

    public setfooterLabel(footerLabel: string, footerIcon?: string) {
        this.pageFooter.footerLabel = `<img src="${footerIcon}" alt="footerLabelIcon"/> <i>${footerLabel}</i>`;
    }

    public clearFooter(){
        this.setFooter( {} as Footer);
    }
}
