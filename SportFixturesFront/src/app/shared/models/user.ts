import { Team } from ".";

export class User {
    public id: string;
    public name: string;
    public lastName: string;
    public email: string;
    public token: string;
    public username: string;
    public favorites: Team[];
    public role: number;

    constructor() {
        this.name = "";
        this.lastName = "";
        this.email = "";
        this.token = "";
        this.username = "";
        this.role = 0;
    }
}