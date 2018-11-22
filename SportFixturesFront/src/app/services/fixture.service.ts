import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";
import { Encounter, Results } from "../shared/models";

@Injectable()
export class FixtureService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getFixtureGenerators(): Promise<string[]> {
        return await this.getAll(AppSettings.ApiEndpoints.getFixtureGenerators);
    }

    public async generateFixture(results: any): Promise<any> {
        return await this.post(AppSettings.ApiEndpoints.addResults, results);
    }
}