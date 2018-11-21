import { Favorite } from "./favorite";
import { Team } from ".";

export class Encounter {
    public id: string;
    public date: Date;
    public sportId: string;
    public favorites: Favorite[];
    public teams: Team[];
    // public score: Score;

    constructor() {
    }
}