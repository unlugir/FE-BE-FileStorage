import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { HomeComponent } from './home/home.component';
import { TokenGuard } from './services/tokenGuard';


const routes: Routes = [
  {path:"login", component: LoginComponent},
  {path:"register", component: RegistrationComponent},
  {path:"home", component: HomeComponent, canActivate: [TokenGuard]},
  {path:"", redirectTo:"/home", pathMatch:"full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
