import { Component, OnInit } from '@angular/core';
import { Sport } from 'src/app/shared/models';
import { EncounterService, SportService } from 'src/app/services';

@Component({
  selector: 'app-fixture-generator',
  templateUrl: './fixture-generator.component.html',
  styleUrls: ['./fixture-generator.component.css']
})
export class FixtureGeneratorComponent implements OnInit {
  public sports: Sport[];
  public algorithms: any[];
  public selectedSport: Sport;
  public selectedAlgorithm: any;

  constructor(
    private encounterService: EncounterService,
    private sportService: SportService) { }

  ngOnInit() {
    this.getSports();
  }

  private async getSports() {
    this.sports = await this.sportService.getSports();
  }
}
