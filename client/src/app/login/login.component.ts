import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AccountService } from '../services/accountService';
import { Router } from '@angular/router';
import { AppComponent } from '../app.component';
import { User } from '../models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitted: boolean;
  errorOccured: boolean;
  errors: string = null;
  constructor(private builder: FormBuilder, private accountService: AccountService, private router: Router) {
    this.loginForm = this.builder.group({email:'', password:'' });
    this.submitted = false;
    this.errorOccured = false;
    console.log(localStorage.getItem("user"));
  }

  ngOnInit() {
  }
  onSubmit(inputData){
    this.submitted = true;
    console.log(inputData);
    this.accountService.login(inputData).subscribe(res=> 
      { 
        this.errorOccured = false;
        console.log(res["token"]);
        
        localStorage.setItem("token", res["token"]);
        this.router.navigate(["home"]);
      }, err=>
      {
        this.submitted = false;
        console.log(err["errors"]);
        this.errorOccured = true;
        this.errors="Invalid Credentials";
      }); 
      
  }
  toRegistration(){
    console.log("To Registration");
    this.router.navigate(["register"]);
  }
}
