import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { GenericHttpService } from './genericHttpService';
import { ApplicationUser } from 'src/app/_models/application-user';
import { LoginUser } from '../_models/login-user-model';

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends GenericHttpService<any>{
    getClients() {
        throw new Error('Method not implemented.');
    }
    protected baseUrl = `${environment.urlApi}`;
    protected baseSite = `${environment.urlApi}`;
    public currentUser: BehaviorSubject<any>;

    constructor(private http: HttpClient) {
        super(http);
        this.currentUser = new BehaviorSubject<any>(JSON.parse(localStorage.getItem('srf_user')));
    }

    registerClient(user: ApplicationUser) {
        return this.postAll('account/registerClient', user);
    }

    registerMaster(user: LoginUser) {
        return this.postAll('account/registerMaster', user);
    }

    logout() {
        localStorage.removeItem('srf_user');
        this.currentUser.next(null);
    }

    addCurrenUser(user) {
        localStorage.setItem('srf_user', JSON.stringify(user));
    }

    clearUser() {
        localStorage.removeItem('srf_user');
    }

    getCurrentUser() {
        return new BehaviorSubject<any>(JSON.parse(localStorage.getItem('srf_user'))).getValue();
    }



    // getUserMaster() {
    //     return new BehaviorSubject<any>(JSON.parse(localStorage.getItem('srf_user_master'))).getValue();
    // }

    loginMaster(user) {
        return this.postAll('account/login', user);
    }


    // logoutClient() {
    //     localStorage.removeItem('srf_user_client');
    //     this.currentUser.next(null);
    // }

    // addUserClient(user) {
    //     localStorage.setItem('srf_user_client', JSON.stringify(user));
    // }

    // clearUserClient() {
    //     localStorage.removeItem('srf_user_client');
    // }



    // getUserClient() {
    //     return new BehaviorSubject<any>(JSON.parse(localStorage.getItem('srf_user_client'))).getValue();
    // }

    // getCurrentUser() {
    //     return new BehaviorSubject<any>(JSON.parse(localStorage.getItem('srf_user_client'))).getValue();
    // }

    // clearUser() {
    //     localStorage.removeItem('srf_user_client');
    // }

    // addCurrenUser(user) {
    //     localStorage.setItem('srf_user_client', JSON.stringify(user));
    // }

    getClientsStore() {
        return this.http.get<any>(`${this.getUrlApi()}account/getClients`);
    }

}
