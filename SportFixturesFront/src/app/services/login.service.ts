import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map'
import { LoginModel } from '../shared/models/login';
import { AppSettings } from '../config/appSettings';
import { SessionService } from './session.service';

@Injectable()
export class LoginService {

    constructor(private http: Http, private sessionService: SessionService) { }

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
                    this.sessionService.setToken(body.token);
                    this.sessionService.setUser(body);
                    return body;
                }
            });
    }

    logout() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let opt = new RequestOptions({ headers: headers, withCredentials: true });
        this.http.post(AppSettings.ApiEndpoints.logout, {}, opt)
            .subscribe(response => {
                let res = response;
            });
        this.sessionService.logout();
    }

}
