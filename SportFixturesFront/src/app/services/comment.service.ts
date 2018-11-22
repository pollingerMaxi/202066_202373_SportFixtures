import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";
import { Comment } from "../shared/models";

@Injectable()
export class CommentService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getCommentsOfEncounter(encounterId: string): Promise<Comment[]> {
        return await this.getAll(AppSettings.ApiEndpoints.getCommentsOfEncounter + encounterId);
    }

    public async addComment(comment: Comment) {
        return await this.post(AppSettings.ApiEndpoints.addComment, comment);
    }

}