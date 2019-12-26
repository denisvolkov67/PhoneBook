import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApiModule } from './phonebook/api.module';

@NgModule({
  declarations: [
    AppComponent,
    NotExistsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule.forRoot(null)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
