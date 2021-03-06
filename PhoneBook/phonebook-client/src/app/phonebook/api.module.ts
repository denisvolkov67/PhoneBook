import { FavoritesService } from './api/favorites.service';
import { EmployeeImportComponent } from './components/employee-import/employee-import.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AuthService } from './api/auth.service';
import { EmployeesService } from './api/employees.service';
import { DepartmentsService } from './api/departments.service';
import { NotExistsComponent } from './components/not-exists/not-exists.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { SearchComponent } from './components/search/search.component';
import { DepartmentComponent } from './components/department/department.component';
import { EmployeeEditComponent } from './components/employee-edit/employee-edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeCreateComponent } from './components/employee-create/employee-create.component';
import { EmployeeInfoComponent } from './components/employee-info/employee-info.component';
import { FavoritesComponent } from './components/favorites/favorites.component';

@NgModule({
  declarations: [
    HomeComponent,
    DepartmentComponent,
    SearchComponent,
    NotExistsComponent,
    EmployeeEditComponent,
    EmployeeCreateComponent,
    EmployeeInfoComponent,
    EmployeeImportComponent,
    FavoritesComponent
  ],
  imports:
  [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    OAuthModule.forRoot()
  ],
  exports: [
    RouterModule,
    ReactiveFormsModule,
    HomeComponent
  ],
  providers: [
    AuthService,
    DepartmentsService,
    EmployeesService,
    FavoritesService
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
