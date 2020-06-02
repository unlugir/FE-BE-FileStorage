import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AccountService } from '../services/accountService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registrationForm: FormGroup;
  submitted: boolean;
  errorOccured: boolean;
  errors: string = null;
  constructor(private builder: FormBuilder, private accountService: AccountService, private router: Router) {
    this.registrationForm = this.builder.group({email:'', password:'' });
    this.submitted = false;
    this.errorOccured = false;
  }

  ngOnInit() {
  }
  onSubmit(inputData){
    this.submitted = true;
    console.log(inputData);
    this.accountService.register(inputData).subscribe(res =>
      {
        console.log(res);
        this.router.navigate(["login"]);
      },err =>{
        console.log(err);
        this.submitted = false;
        this.errorOccured = true;
        this.errors="Invalid credentials"
      }); 
  }
  toLogin(){
    console.log("To Login");
    this.router.navigate(["login"]);
  }
}
