import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";

@Injectable()
export class UserService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getAllUsers() {
        return await this.getAll(AppSettings.ApiEndpoints.getAllUsers);
    }
}