import { Component, OnInit } from '@angular/core';
import { Encounter, Results, PositionInEncounter } from 'src/app/shared/models';
import { ToasterService } from 'angular2-toaster';
import { EncounterService } from 'src/app/services';

@Component({
  selector: 'app-positions-create',
  templateUrl: './positions-create.component.html',
  styleUrls: ['./positions-create.component.css']
})
export class PositionsCreateComponent implements OnInit {
  public result: Results;
  public encounters: Encounter[];
  public positionInEncounter: PositionInEncounter;
  public selectedEncounter: Encounter;
  public checked: boolean;
  public positions: any[] = [];
  public multiPos: PositionInEncounter[] = [];

  constructor(
    private toasterService: ToasterService,
    private encounterService: EncounterService) {
    this.selectedEncounter = new Encounter();
    this.positionInEncounter = new PositionInEncounter();
  }

  ngOnInit() {
    this.getEncounters();
  }

  public async getEncounters() {
    this.encounters = await this.encounterService.getEncounters();
  }

  public addResults() {
    let result = new Results();
    result.encounterId = parseInt(this.selectedEncounter.id);
    result.positions = this.multiPos;
    this.encounterService.addResults(result)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Results successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  onChange(event) {
    this.positions = [];
    let i = 1;
    this.selectedEncounter.teams.forEach(et => {
      this.positions.push({ position: i });
      i++;
    });
  }

  onPositionChange(event, teamId) {
    try {
      let found = this.multiPos.find(t => t.teamId == teamId);
      if (found) {
        found.position = event.value
      } else {
        let pos = new PositionInEncounter();
        pos.position = event.value;
        pos.teamId = teamId;
        this.multiPos.push(pos);
      }

    } catch (error) {
      console.log(error)
    }
  }

}
