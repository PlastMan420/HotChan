import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements OnDestroy {

constructor() {
    this.initSession();
 }
    ngOnDestroy(): void {
        this.activeSession$.complete();
    }

    private _payLoad!: JWTexPayload | null;
    private _expiresOn!: number | null;
    private _userName!: string | null;
    private _jti!: string | null;
    private _userId!: string | null;
    private _activeSession: boolean = false;
    activeSession$: Subject<boolean> = new Subject<boolean>();

    public get hasSessionExpired(): boolean {
        // If no valid date in JWT. expire and revoke.
        return (this._expiresOn ?? -8640000000000000) >= Date.now();
    }

    public get jti() {return this._jti;}
    public get userId() {return this._userId;}
    public get userName() {return this._userName;}
    public get activeSession() {return this._activeSession;}

    public userLogin(jwt: string) {
        localStorage.setItem('userLogin', jwt);
        this.initSession();
        this.activeSession$.next(true);
    }

    public userLogout() {
        localStorage.removeItem('userLogin');
        this.clearSession();
    }

    private readJWT(jwt = localStorage.getItem("userLogin")) {
        if(!jwt)
        {
            return null;
        }
        
        return JSON.parse(window.atob(jwt.split('.')[1]));
    }

    private validateJWT() {
        if(this.hasSessionExpired){
            this.userLogout();
        }
    }

    private initSession() {
        this._payLoad = this.readJWT();
        if(this._payLoad){
            this._expiresOn = this._payLoad.exp ?? -8640000000000000;

            if(this._expiresOn <= (Date.now() / 1000)){
                this.userLogout();
                return;
            }

            this._userName = this._payLoad.name;
            this._jti = this._payLoad.jti as string;
            this._userId = this._payLoad.sub as string;
            this._activeSession = true;
            this.activeSession$.next(this._activeSession);
        }

    }

    private clearSession(){
        this._payLoad = null;
        this._expiresOn = -8640000000000000;
        this._userName = null;
        this._jti = null;
        this._userId = null;
        this._activeSession = false;
        this.activeSession$.next(this._activeSession);
    }
}

// standard names https://www.rfc-editor.org/rfc/rfc7515.html#section-4.1
interface JwtHeader {
    alg: string | Algorithm;
    typ?: string | undefined;
    cty?: string | undefined;
    crit?: Array<string | Exclude<keyof JwtHeader, 'crit'>> | undefined;
    kid?: string | undefined;
    jku?: string | undefined;
    x5u?: string | string[] | undefined;
    'x5t#S256'?: string | undefined;
    x5t?: string | undefined;
    x5c?: string | string[] | undefined;
}

// standard claims https://datatracker.ietf.org/doc/html/rfc7519#section-4.1
interface JwtPayload {
    [key: string]: any;
    iss?: string | undefined;
    sub?: string | undefined;
    aud?: string | string[] | undefined;
    exp?: number | undefined;
    nbf?: number | undefined;
    iat?: number | undefined;
    jti?: string | undefined;
}

interface Jwt {
    header: JwtHeader;
    payload: JwtPayload;
    signature: string;
}

interface JWTexPayload extends JwtPayload {
    name: string;
    email: string;
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
    role: string;
};

