import { User } from "./user";
import { Team } from "./team";

export class Favorite {
    public userId: string;
    public user: User;
    public teamId: string;
    public team: Team;

    constructor() {
    }
}