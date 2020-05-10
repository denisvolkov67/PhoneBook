import { environment } from './../../../environments/environment';
import { Router} from '@angular/router';
import { Injectable, Output, EventEmitter } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { log } from 'util';
import { filter } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

export const authConfig: AuthConfig = {

  // Url of the Identity Provider
  issuer: environment.issuer,

  // URL of the SPA to redirect the user to after login
  redirectUri: environment.redirectUri,

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: 'spa',

  // responseType: 'id_token token',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile phonebook_api',

  postLogoutRedirectUri: environment.postLogoutRedirectUri
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  @Output() tokenValidState = new EventEmitter<boolean>();

  constructor(private oauthService: OAuthService, private router: Router) {
    this.oauthService.configure(authConfig);
    this.oauthService.setStorage(sessionStorage);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();

    this.oauthService.events
      .pipe(filter(e => e.type === 'token_received'))
      .subscribe(_ => { this.updateToken(); });
  }

  isTokenValid() {

    const jwt = sessionStorage.getItem('id_token');
    console.log("id_token");
    console.log(jwt);
    console.log("access_token");
    console.log(sessionStorage.getItem('access_token'));
    if (jwt == null) { return false; }
    else { return true; }
  }

  getValueFromIdToken(claim: string) {
    const jwt = this.oauthService.getIdToken();
    if (jwt == null) {
      return null;
    }
    const jwtData = jwt.split('.')[1];
    const helper = new JwtHelperService();
    const decodedJwtJsonData = helper.urlBase64Decode(jwtData);
    let value: any;
    JSON.parse(decodedJwtJsonData, function findKey(k, v) {
      if (k === claim) {
        value = v;
      }
    });
    return value;
  }


  loginUser() {
    this.oauthService.initImplicitFlow();
  }

  logoutUser() {
    this.oauthService.logOut();
  }

  updateToken() {
    log('token_received in auth service');
    this.tokenValidState.emit(true);
  }

  hasValidAccessToken() {
    return this.oauthService.hasValidAccessToken();
  }
}

