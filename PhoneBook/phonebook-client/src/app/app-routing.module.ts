import { HomeComponent } from './phonebook/components/home/home.component';
import { UserComponent } from './phonebook/components/user/user.component';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'user/:login', component: UserComponent},
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotExistsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
