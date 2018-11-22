import { Component, OnInit } from '@angular/core';
import { Sport, FixtureEncounter, Fixture, Encounter, EncountersTeams } from 'src/app/shared/models';
import { EncounterService, SportService, FixtureService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';

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
    private fixtureService: FixtureService,
    private toasterService: ToasterService) { }

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
  }

  public addFixture() {
    let encounters = this.makeEncounters();
    this.encounterService.addEncountersInBulk(encounters)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Encounters successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  private makeEncounters() {
    let encounters = new Array<Encounter>();
    this.fixtureEncounters.forEach(fenc => {
      let enc = new Encounter();
      enc.teams = new Array<EncountersTeams>();
      enc.date = fenc.date;
      enc.sportId = fenc.sportId.toString();
      fenc.teams.forEach(team => {
        let encTeam = new EncountersTeams();
        encTeam.team = team.team;
        encTeam.teamId = parseInt(team.team.id);
        encTeam.team.sportId = team.team.sportId;
        enc.teams.push(encTeam);
      });
      encounters.push(enc);
    });
    return encounters;
  }
}
