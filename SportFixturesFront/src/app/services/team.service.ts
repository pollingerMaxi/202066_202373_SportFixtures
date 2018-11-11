import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";
import { Team } from "../shared/models";

@Injectable()
export class TeamService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getTeams(): Promise<any> {
        return await this.getAll(AppSettings.ApiEndpoints.getTeams);
    }

    public async addTeam(team: Team) {
        return await this.post(AppSettings.ApiEndpoints.addTeam, team);
    }

    public async updateTeam(team: Team) {
        return await this.update(AppSettings.ApiEndpoints.updateTeam, team);
    }

    public async deleteTeam(id: string) {
        return await this.delete(AppSettings.ApiEndpoints.deleteTeam, id);
    }
}