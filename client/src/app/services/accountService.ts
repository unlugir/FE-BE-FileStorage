import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
    providedIn: 'root',
  })
export class AccountService{
    apiUrl = "https://localhost:5001/";
    constructor(private http: HttpClient){

    }

    private getHeaders(){
        let headers = new HttpHeaders();
        if (localStorage.getItem("token")){
            console.log("CREATING AUTH HEADER");
            
            headers = new HttpHeaders({"Authorization":"Bearer "+localStorage.getItem("token")});
        }
        headers.append('Access-Control-Allow-Headers', 'Content-Type');
        headers.append('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
        headers.append('Access-Control-Allow-Origin', '*');
        return headers;
    }

    public login(loginData){
        return this.http.post(this.apiUrl+"api/account/login", loginData, {headers: this.getHeaders()});
    }

    public register(regData){
        return this.http.post(this.apiUrl+"api/account/register", regData);
    }

    public getCurrentUser():Observable<User>{
        console.log("Current Token");
        console.log(localStorage.getItem("token"));
        return this.http.get<User>(this.apiUrl+"api/user",{headers:this.getHeaders()});
    }
    
    public clearStorage(){
        localStorage.removeItem("token");
    }
}