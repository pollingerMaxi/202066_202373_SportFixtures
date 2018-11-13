import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";
import { Encounter } from "../shared/models";

@Injectable()
export class EncounterService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getEncountersOfTeam(teamId: string): Promise<Encounter[]> {
        return await this.getAll(AppSettings.ApiEndpoints.getEncountersOfTeam + teamId);
    }
}