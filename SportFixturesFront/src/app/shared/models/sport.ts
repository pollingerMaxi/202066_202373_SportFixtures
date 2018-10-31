import { Team } from "./team";
import { EncounterMode } from "./encounterMode";

export class Sport {
    public id: string;
    public name: string;
    public encounterMode: EncounterMode;
    public teams: Team[];

    constructor() {
        this.name = "";
        this.encounterMode = EncounterMode.Double;
        this.teams = [];
    }
}