import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { AppSettings } from "../config/appSettings";
import { User, Role } from "../shared/models";

@Injectable()
export class UserService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public async getAllUsers() {
        return await this.getAll(AppSettings.ApiEndpoints.getAllUsers);
    }

    public async getFavoritesOfUser(id: string) {
        return await this.getAll(AppSettings.ApiEndpoints.getFavorites + id);
    }

    public async addUser(user: User) {
        return await this.post(AppSettings.ApiEndpoints.addUser, user);
    }

    public async updateUser(user: User) {
        return await this.update(AppSettings.ApiEndpoints.updateUser, user);
    }

    public async deleteUser(id: string) {
        return await this.delete(AppSettings.ApiEndpoints.deleteUser, id);
    }

    public getRoles() {
        var roles = [
            { label: Role.User, value: Role.User },
            { label: Role.Admin, value: Role.Admin }
        ];
        return roles;
    }
}