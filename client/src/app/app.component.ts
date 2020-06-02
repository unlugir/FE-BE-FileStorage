import { Component } from '@angular/core';
import { AccountService } from './services/accountService';
import { User } from './models/user';
import { HttpClientJsonpModule } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Storage';
  static user:User;
  static userLoggedIn: boolean = false;

  getUser(){
    return AppComponent.user;
  }
  userIsLogged(){
    return AppComponent.userLoggedIn;
  }
  constructor(private accountService: AccountService,private router:Router){
    
  }
  logout(){
    AppComponent.userLoggedIn = false;
    this.accountService.clearStorage();
    this.router.navigate(["login"]);
  }
}
