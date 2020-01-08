import { SearchComponent } from './phonebook/components/search/search.component';
import { AdministrativeStaffComponent } from './phonebook/components/administrative-staff/administrative-staff.component';
import { HomeComponent } from './phonebook/components/home/home.component';
import { UserComponent } from './phonebook/components/user/user.component';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'home/administrative-staff', component: AdministrativeStaffComponent},
  {path: 'user/:login', component: UserComponent},
  {path: 'users/:name', component: SearchComponent},
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotExistsComponent }
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
