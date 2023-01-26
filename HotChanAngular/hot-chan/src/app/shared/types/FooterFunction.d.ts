export type FooterFunction = {
    label: string;
    func: Function;
    funcName: string;
};

export type Footer = {
    pageFunctions: FooterFunction[];
    context: object;
    footerLabel: string
}
