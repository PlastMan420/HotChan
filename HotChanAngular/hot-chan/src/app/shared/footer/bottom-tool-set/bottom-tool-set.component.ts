import { Component, OnInit } from '@angular/core';
import { FooterFunction } from '../../types/FooterFunction';
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
    functions!: FooterFunction[];
    persistant!: FooterFunction[];

    ngOnInit() {
        this.initTimer();
        this.setFooterFunctions();

        this.footerServ.functionsChangeDetector.subscribe(x => {
            this.setFooterFunctions();
        });
    }

    private setFooterFunctions() {
        this.functions = this.footerServ.pageFunctions;
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
        this.footerServ.calledFn(fn);
    }
}
