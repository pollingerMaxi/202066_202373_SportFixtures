import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map'
import { LoginModel } from '../shared/models/login';
import { AppSettings } from '../config/appSettings';

@Injectable()
export class LoginService {

    constructor(private http: Http) { }

    /**
     * Login with credentials
     * @param credentials Email & Password
     */
    login(credentials: LoginModel) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Access-Control-Allow-Origin', '*');
        let opt = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.post(AppSettings.ApiEndpoints.login, JSON.stringify(credentials), opt)
            .map((response: Response) => {
                let body = response.json();
                if (body) {
                    console.log("loggedIn");
                    return body;
                }
            });
    }

    logout() {
        console.log("logout");
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let opt = new RequestOptions({ headers: headers, withCredentials: true });
        this.http.post(AppSettings.ApiEndpoints.logout, {}, opt)
            .subscribe(response => {
                let res = response;
            });
    }

}
