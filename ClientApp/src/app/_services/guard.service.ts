import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable()
export class Guard implements CanActivate {

    constructor(protected router: Router, protected auth: AuthenticationService ) {}

     canActivate() {
        if (localStorage.getItem('surfalianca_user')) {
            // logged in so return true
            return true;
        }
        // not logged in so redirect to login page
        this.router.navigate(['/home']);
        return false;
    }
}
