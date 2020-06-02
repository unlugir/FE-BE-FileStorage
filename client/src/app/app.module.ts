import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NotifierModule } from "angular-notifier";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TestComponent } from './test/test.component';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { AccountService } from './services/accountService';
import { HomeComponent } from './home/home.component';
import { FileService } from './services/fileService';
import { TokenGuard } from './services/tokenGuard';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent

    
  ],
  imports: [
    NotifierModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [AccountService, FileService,TokenGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
