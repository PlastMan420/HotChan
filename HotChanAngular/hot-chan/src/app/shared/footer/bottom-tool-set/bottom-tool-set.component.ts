import { Component, OnInit } from '@angular/core';
import { Footer, FooterFunction } from '../../types/FooterFunction';
import { FooterService } from '../footer.service';

@Component({
    selector: 'app-bottom-tool-set',
    templateUrl: './bottom-tool-set.component.html',
    styleUrls: ['./bottom-tool-set.component.scss'],
})
export class BottomToolSetComponent implements OnInit {
    constructor(private footerServ: FooterService) {}

    today = new Date();
    todaysDataTime!: string;
    pageFooter!: Footer;
    persistant!: FooterFunction[];

    ngOnInit() {
        this.initTimer();
        this.setFooterFunctions();

        this.footerServ.functionsChangeDetector.subscribe({ next: x => {
            this.setFooterFunctions();
        } });
    }

    private setFooterFunctions() {
        this.pageFooter = this.footerServ.pageFooter;
    }

    private initTimer() {
        setInterval(() => {
            const bleh = new Date();
            this.todaysDataTime =
                bleh.getHours() +
                ':' +
                bleh.getMinutes();
        }, 1);
    }

    public calledFn(fn: FooterFunction) {
        const event = {context: this.pageFooter.context, pageFunctions: [fn]} as Footer;

        this.footerServ.calledFn(event);
    }
}
