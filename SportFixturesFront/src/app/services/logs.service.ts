import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";

@Injectable()
export class LogsService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getLogs(from: string, to:string) {
        let url = AppSettings.ApiEndpoints.getLogs.replace("{0}", from).replace("{1}", to);
        let files = await this.getAll(url);
        return files;
    }
}
