import { Inject, Injectable } from '@angular/core';
import { FooterService } from './footer/footer.service';
import { FooterFunction } from './types/FooterFunction';

@Injectable({
    providedIn: 'root',
})
export class HotchanService {
    constructor(private footerServ: FooterService) {
        
    }


}
