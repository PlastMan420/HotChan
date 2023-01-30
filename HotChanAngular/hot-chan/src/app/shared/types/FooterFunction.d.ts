import { Subject } from "rxjs";

export type FooterFunction = {
    label?: string;
    buttonIcon?: string;
    buttonIconClass?: string;
    func?: Function;
    funcName?: string;
    funcParams?: any[]
    type: string;
    dataStream?: Subject<string>;
    class?: string;
};

export type Footer = {
    pageFunctions: FooterFunction[];
    context: object;
    footerLabel: string
};
