import { Team } from ".";
import { Role } from "./role";

export class User {
    public id: string;
    public name: string;
    public lastName: string;
    public email: string;
    public token: string;
    public username: string;
    public favorites: Team[];
    public role: Role;
    public password: string;

    constructor() {
        this.name = "";
        this.lastName = "";
        this.email = "";
        this.token = "";
        this.username = "";
        this.role = Role.User;
        this.password = "";
    }
}