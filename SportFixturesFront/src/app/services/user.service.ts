import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { Observable } from "rxjs";
import { AppSettings } from "../config/appSettings";

@Injectable()
export class UserService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public getAllUsers(): Observable<any> {
        return this.getAll(AppSettings.ApiEndpoints.getAllUsers);
    }
}