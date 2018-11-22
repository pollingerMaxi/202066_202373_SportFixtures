import { Component, OnInit } from '@angular/core';
import { Sport, FixtureEncounter, Fixture } from 'src/app/shared/models';
import { EncounterService, SportService, FixtureService } from 'src/app/services';

@Component({
  selector: 'app-fixture-generator',
  templateUrl: './fixture-generator.component.html',
  styleUrls: ['./fixture-generator.component.css']
})
export class FixtureGeneratorComponent implements OnInit {
  public dateFormat: String = "dd-MM-yyyy";
  public sports: Sport[];
  public algorithms: any[] = [];
  public selectedSport: Sport;
  public selectedAlgorithm: any;
  public fixtureEncounters: FixtureEncounter[] = [];
  public selectedDate: Date;

  constructor(
    private encounterService: EncounterService,
    private sportService: SportService,
    private fixtureService: FixtureService) { }

  ngOnInit() {
    this.getSports();
    this.getAlgorithmNames();
  }

  private async getSports() {
    this.sports = await this.sportService.getSports();
  }

  private async getAlgorithmNames() {
    this.algorithms = await this.fixtureService.getFixtureGenerators();
  }

  public async generateFixture() {
    let fixture = new Fixture();
    fixture.sportId = parseInt(this.selectedSport.id);
    fixture.algorithmName = this.selectedAlgorithm.name;
    fixture.date = this.selectedDate
    this.fixtureEncounters = await this.fixtureService.generateFixture(fixture);
    console.log(this.fixtureEncounters);
  }
}
