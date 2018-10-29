import { Component, OnInit } from '@angular/core';
import { Sport } from '../shared/models/sport';
import { EncounterMode } from '../shared/models/encounterMode';
import { SelectItem } from 'primeng/components/common/selectitem';
import { ToasterService } from 'angular2-toaster';
import { SportService } from '../services';

@Component({
  selector: 'app-sports-management',
  templateUrl: './sports-management.component.html',
  styleUrls: ['./sports-management.component.css']
})
export class SportsManagementComponent implements OnInit {
  public sport: Sport;
  encounterModes: SelectItem[];
  selectedEncounterMode: EncounterMode;

  constructor(
    private sportService: SportService,
    private toasterService: ToasterService) {
    this.sport = new Sport();
  }

  ngOnInit() {
    this.encounterModes = this.sportService.getEncounterModes();
  }

  public addSport(sport: Sport) {
    this.sportService.addSport(sport)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Sport successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", "Could not add sport.");
      });
  }

}
