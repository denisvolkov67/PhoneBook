import { EmployeesService } from './api/employees.service';
import { DepartmentsService } from './api/departments.service';
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
import { SearchComponent } from './components/search/search.component';
import { DepartmentComponent } from './components/department/department.component';

@NgModule({
  declarations: [
    HomeComponent,
    UserComponent,
    DepartmentComponent,
    SearchComponent,
    NotExistsComponent
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
    DepartmentsService,
    EmployeesService,
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
