import { Favorite } from "./favorite";

export class Encounter {
    public id: string;
    public date: Date;
    public sportId: string;
    public favorites: Favorite[];
    // public score: Score;

    constructor() {
    }
}