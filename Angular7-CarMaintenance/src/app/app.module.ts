import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms' 
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserService } from './shared/user.service';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { DevExtremeModule } from 'devextreme-angular';
import { NavbarComponent } from './shared/navbar.component';
import { AddCarComponent } from './cars/add-car/add-car.component';
import { ListCarsComponent } from './cars/list-cars/list-cars.component';
import { HomeCarsComponent } from './cars/home-cars/home-cars.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomDatepickerComponent } from './shared/custom-datepicker/custom-datepicker.component';
import { YearPickerComponent } from './shared/custom-datepicker/year-picker-component/year-picker-component.component';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatInputModule } from '@angular/material/input';
import { PeriodicityComponent } from './periodicity/periodicity.component';
@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    HomeComponent,
    AdminPanelComponent,
    ForbiddenComponent,
    NavbarComponent,
    AddCarComponent,
    ListCarsComponent,
    HomeCarsComponent,
    CustomDatepickerComponent,
    YearPickerComponent,
    PeriodicityComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    DevExtremeModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule,
    NgbModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatMomentDateModule,
    MatInputModule
  ],
  providers: [UserService,{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
