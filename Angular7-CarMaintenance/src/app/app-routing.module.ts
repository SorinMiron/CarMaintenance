import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { HomeCarsComponent } from './cars/home-cars/home-cars.component';
import { PeriodicityComponent } from './periodicity/periodicity.component';
import { ServiceCalendarComponent } from './service-calendar/service-calendar.component';



const routes: Routes = [
  {
    path:'user', component:UserComponent, 
      children: [
        {path:'registration', component:RegistrationComponent},
        {path:'login', component:LoginComponent}

      ]
  },
  {
    path:'home', component:HomeComponent, canActivate:[AuthGuard]
  },
  {
    path:'forbidden', component:ForbiddenComponent
  },
  {
    path:'adminpanel', component:AdminPanelComponent, canActivate:[AuthGuard], data: {permittedRoles:['Admin']}
  },
  {
    path:'mycars', component:HomeCarsComponent, canActivate:[AuthGuard], data: {permittedRoles:['Customer']}
  },
  {
    path:'periodicity', component:PeriodicityComponent, canActivate:[AuthGuard], data: {permittedRoles:['Customer']}
  },
  {
    path:'serviceCalendar', component:ServiceCalendarComponent, canActivate:[AuthGuard], data: {permittedRoles:['Customer']}
  },
  {
    path:'**', redirectTo:'/user/login', pathMatch:'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
