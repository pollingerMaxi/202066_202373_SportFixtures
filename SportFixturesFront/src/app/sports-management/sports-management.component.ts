import { Component, OnInit } from '@angular/core';
import { Sport } from '../shared/models/sport';
import { EncounterMode } from '../shared/models/encounterMode';
import { SelectItem } from 'primeng/components/common/selectitem';

@Component({
  selector: 'app-sports-management',
  templateUrl: './sports-management.component.html',
  styleUrls: ['./sports-management.component.css']
})
export class SportsManagementComponent implements OnInit {
  public sport: Sport;
  encounterModes: SelectItem[];
  selectedEncounterMode: EncounterMode;

  constructor() {
    this.sport = new Sport();
  }

  ngOnInit() {
    this.encounterModes = [
      { label: EncounterMode.Double, value: EncounterMode.Double },
      { label: EncounterMode.Multiple, value: EncounterMode.Multiple }
    ];
  }

  public addSport(sport: Sport) {
    console.log(sport);
  }

}
