import { WinAuthInterceptor } from './phonebook/class/WinAuthInterceptor';

import { Routes, RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApiModule } from './phonebook/api.module';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HTTP_INTERCEPTORS } from '@angular/common/http';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule.forRoot(null),
    OAuthModule.forRoot()
  ],
  providers: [
    {
        provide: HTTP_INTERCEPTORS,
        useClass: WinAuthInterceptor,
        multi: true
    }
    // { provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
