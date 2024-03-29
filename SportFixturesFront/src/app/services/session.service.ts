import { Injectable } from "@angular/core";
import { AppSettings } from "../config/appSettings";
import { User } from "../shared/models";

@Injectable()
export class SessionService {
    public setUser(user) {
        localStorage.setItem(AppSettings.localstorageUser, JSON.stringify(user));
    }

    public setToken(token) {
        localStorage.setItem(AppSettings.localstorageToken, JSON.stringify(token));
    }

    public getUser(): User {
        let user = localStorage.getItem(AppSettings.localstorageUser);
        return <User>JSON.parse(user);
    }

    public getToken(): string {
        return localStorage.getItem(AppSettings.localstorageToken);
    }

    public logout() {
        localStorage.removeItem(AppSettings.localstorageUser);
        localStorage.removeItem(AppSettings.localstorageToken);
    }

    public isAdmin(): boolean {
        let user = JSON.parse(localStorage.getItem(AppSettings.localstorageUser));
        return user.role == 1;
    }

    public userIsLogged(): boolean {
        let token = JSON.parse(localStorage.getItem(AppSettings.localstorageUser));
        return token != null || token != undefined;
    }
}