import { Injectable } from "@angular/core";
import { CanActivate } from "@angular/router";
import { AuthenticationService } from "../services/Authentication.service";

@Injectable()
class IsLoggedInGuard implements CanActivate {
  constructor(private authService: AuthenticationService) {};

  canActivate() {
		const passing = 
			!this.authService.hasSessionExpired &&
			!!this.authService.jti &&
			!!this.authService.userId && 
			!!this.authService.userName;

    if (passing) {
      return true;
    }
		else {
      return false;
    }
  }
}