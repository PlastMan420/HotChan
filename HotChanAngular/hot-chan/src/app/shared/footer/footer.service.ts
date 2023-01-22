import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { FooterFunction } from '../types/FooterFunction';

@Injectable({
    providedIn: 'root',
})
export class FooterService {
    constructor() {}

    private _pageFunctions!: FooterFunction[];
    public functionsChangeDetector = new Subject();
    public callFn = new Subject<FooterFunction>();
    pageFooter!: FooterFunction[];

    callFn$ = this.callFn.asObservable();

    private set pageFunctions(fun: FooterFunction[]) {
        this._pageFunctions = fun;
        this.functionsChangeDetector.next(this._pageFunctions);
    }

    public get pageFunctions(): FooterFunction[] {
        return this._pageFunctions;
    }

    public calledFn(fn: FooterFunction){
        
       // console.log(fn)
        this.callFn.next(fn);
    }

    public setFooter(footerFunctions: FooterFunction[]) {
        this.pageFooter = footerFunctions;
        this.pageFunctions = this.pageFooter;
      }
  
    public appendFooterFunction(footerFunction: FooterFunction){
        this.pageFooter.push(footerFunction);
        this.pageFunctions = this.pageFooter;
    }
}
