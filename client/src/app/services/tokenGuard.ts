import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class TokenGuard implements CanActivate {
    constructor(private router:Router){
        
    }
    canActivate(next:ActivatedRouteSnapshot,state: RouterStateSnapshot):boolean {
        if(localStorage.getItem("token")){
            return true;
        }
        this.router.navigate(["login"]);
        return false;
    }
}