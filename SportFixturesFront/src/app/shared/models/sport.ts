import { EncounterMode } from "./encounterMode";

export class Sport {
    public name: string;
    public encounterMode: EncounterMode;

    constructor() {
        this.name = "";
        this.encounterMode = EncounterMode.Double
    }
}