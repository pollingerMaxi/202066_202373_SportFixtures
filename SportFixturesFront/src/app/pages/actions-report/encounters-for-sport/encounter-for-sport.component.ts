import { Component, OnInit } from '@angular/core';
import { Sport, Encounter } from 'src/app/shared/models';
import { ToasterService } from 'angular2-toaster';
import { SportService, EncounterService } from 'src/app/services';

@Component({
  selector: 'app-encounter-for-sport',
  templateUrl: './encounter-for-sport.component.html',
  styleUrls: ['./encounter-for-sport.component.css']
})
export class EncounterForSportComponent implements OnInit {
  public sports: Sport[];
  public selectedSport: Sport;
  public encounters: Encounter[];

  constructor(
    private toasterService: ToasterService,
    private sportService: SportService,
    private encounterService: EncounterService) {
    this.selectedSport = new Sport();
  }

  ngOnInit() {
    this.getSports()
  }

  public async getSports() {
    this.sports = await this.sportService.getSports();
  }

  async onChange(event) {
    try {
      this.encounters = await this.encounterService.getEncountersOfSport(this.selectedSport.id);
    }
    catch (error) {
      this.encounters = [];
      this.toasterService.pop("warning", "Atention!", "No encounters for selected sport");
    }
  }
}
