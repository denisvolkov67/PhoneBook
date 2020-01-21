import { NotExistsComponent } from './components/not-exists/not-exists.component';
import { User } from './model/user';
import { RouterModule } from '@angular/router';
import { UserComponent } from './components/user/user.component';
import { HomeComponent } from './components/home/home.component';
import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { UserService } from './api/user.service';
import { AdministrativeStaffComponent } from './components/administrative-staff/administrative-staff.component';
import { SearchComponent } from './components/search/search.component';
import { HrDepartmentComponent } from './components/hr-department/hr-department.component';

@NgModule({
  declarations: [
    HomeComponent,
    UserComponent,
    AdministrativeStaffComponent,
    SearchComponent,
    NotExistsComponent,
    HrDepartmentComponent
  ],
  imports:
  [
    CommonModule,
    HttpClientModule,
    RouterModule
  ],
  exports: [
    RouterModule,
    HomeComponent,
    UserComponent
  ],
  providers: [
    UserService
  ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
