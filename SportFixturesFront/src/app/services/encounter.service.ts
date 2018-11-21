import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";
import { Encounter, Results } from "../shared/models";

@Injectable()
export class EncounterService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getEncountersOfTeam(teamId: string): Promise<Encounter[]> {
        return await this.getAll(AppSettings.ApiEndpoints.getEncountersOfTeam + teamId);
    }

    public async addEncounter(encounter: Encounter) {
        return await this.post(AppSettings.ApiEndpoints.addEncounter, encounter);
    }

    public async getEncounters(): Promise<Encounter[]> {
        return await this.getAll(AppSettings.ApiEndpoints.getEncounters);
    }

    public async updateEncounter(encounter: Encounter) {
        return await this.update(AppSettings.ApiEndpoints.updateEncounter, encounter);
    }

    public async deleteEncounter(id: string) {
        return await this.delete(AppSettings.ApiEndpoints.deleteEncounter, id);
    }

    public async addResults(results: Results) {
        return await this.post(AppSettings.ApiEndpoints.addResults, results);
    }
 
    

    public async getEncountersOfSport(sportId: string): Promise<Encounter[]> {
        return await this.getAll(AppSettings.ApiEndpoints.getEncountersOfSport + sportId);
    }
}