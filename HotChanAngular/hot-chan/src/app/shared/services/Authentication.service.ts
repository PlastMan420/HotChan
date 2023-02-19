import { Injectable } from '@angular/core';
import { Jwt, JwtPayload } from 'jsonwebtoken';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

constructor() {
    this._token = this.readJWT();
    this._payLoad = this._token.payload as JWTexPayload;
    this._expiresOn = this._payLoad.exp ?? -8640000000000000;
    this._userName = this._payLoad.name;
    this._jti = this._payLoad.jti as string;
    this._userId = this._payLoad.sub as string;
 }

    private _token: Jwt;
    private _payLoad: JWTexPayload;
    private _expiresOn: number;
    private _userName: string;
    private _jti: string;
    private _userId: string;

    public get hasSessionExpired(): boolean {
        // If no valid date in JWT. expire and revoke.
        return this._expiresOn >= Date.now();
    }

    public get jti() {return this._jti;}
    public get userId() {return this._userId;}
    public get userName() {return this._userName;}

    public userLogin(jwt: string) {
        localStorage.setItem('userLogin', jwt);

        

        if(typeof this._token.payload !== 'string')
        {
            localStorage.setItem('JWT_Expires', this._token.payload.exp?.toString() ?? "" );
        }
        else {
            localStorage.setItem('JWT_PayLoad', this._token.payload );
        }
    }

    public userLogout() {
        localStorage.removeItem('userLogin');
    }

    private readJWT(jwt = localStorage.getItem("userLogin")): Jwt  {
        if(!jwt)
        {
            return {} as Jwt;
        }
        
        return JSON.parse(window.atob(jwt.split('.')[1]));
    }

    private validateJWT() {
        if(this.hasSessionExpired){
            this.userLogout();
        }
    }
}

interface JWTexPayload extends JwtPayload {
    name: string;
    email: string;
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
    role: string;
};