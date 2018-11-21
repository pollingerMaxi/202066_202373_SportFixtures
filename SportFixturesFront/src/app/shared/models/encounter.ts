import { Favorite } from "./favorite";
import { EncountersTeams } from "./encountersTeams";

export class Encounter {
    public id: string;
    public date: Date;
    public sportId: string;
    public favorites: Favorite[];
    public teams: EncountersTeams[];
    // public score: Score;

    constructor() {
    }
}