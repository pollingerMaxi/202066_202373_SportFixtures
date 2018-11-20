import { Component, OnInit } from '@angular/core';
import { Encounter } from 'src/app/shared/models';
import { EncounterService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';

@Component({
  selector: 'app-encounters-management',
  templateUrl: './encounters-management.component.html',
  styleUrls: ['./encounters-management.component.css']
})
export class EncountersManagementComponent implements OnInit {
  public encounter: Encounter;
  public encounters: Encounter[];
  public selectedEncounter: Encounter;

  constructor(
    private encounterService: EncounterService,
    private toasterService: ToasterService) {
      this.encounter = new Encounter();
      this.selectedEncounter = new Encounter();
  }

  ngOnInit() {
    this.getEncounters();
  }

  public addEncounter(encounter: Encounter) {
    this.encounterService.addEncounter(encounter)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Sport successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  private async getEncounters() {
    this.encounters = await this.encounterService.getEncounters();
  }

  public updateSport(encounter: Encounter) {
    this.encounterService.updateEncounter(encounter)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Sport successfully updated!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public deleteSport(id: string) {
    this.encounterService.deleteEncounter(id)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Sport successfully deleted!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });;
  }

}
