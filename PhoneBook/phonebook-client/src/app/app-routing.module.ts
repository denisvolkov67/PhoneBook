import { SearchComponent } from './phonebook/components/search/search.component';
import { HomeComponent } from './phonebook/components/home/home.component';
import { NotExistsComponent } from './phonebook/components/not-exists/not-exists.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DepartmentComponent } from './phonebook/components/department/department.component';


const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'home/department/:id', component: DepartmentComponent},
  {path: 'employees/:name', component: SearchComponent},
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotExistsComponent }
];



@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
