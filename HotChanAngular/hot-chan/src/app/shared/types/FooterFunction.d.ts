import { Observable } from "@apollo/client";
import { Subject } from "rxjs";

export type FooterFunction = {
    label: string;
    func?: Function;
    funcName?: string;
    funcParams?: any[]
    type: string;
    dataStream?: Subject<string>;
};

export type Footer = {
    pageFunctions: FooterFunction[];
    context: object;
    footerLabel: string
};
