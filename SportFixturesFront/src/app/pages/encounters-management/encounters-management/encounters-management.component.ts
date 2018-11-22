import { Component, OnInit } from '@angular/core';
import { Encounter, Sport, Team, EncountersTeams } from 'src/app/shared/models';
import { EncounterService, SportService, TeamService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';

@Component({
  selector: 'app-encounters-management',
  templateUrl: './encounters-management.component.html',
  styleUrls: ['./encounters-management.component.css']
})
export class EncountersManagementComponent implements OnInit {
  public dateFormat: String = "dd-MM-yyyy";
  public encounter: Encounter;
  public encounters: Encounter[];
  public selectedEncounter: Encounter;
  public sports: Sport[];
  public selectedTeams: Team[];
  public teams: Team[];
  public selectedSport: Sport;

  constructor(
    private encounterService: EncounterService,
    private toasterService: ToasterService,
    private sportService: SportService,
    private teamService: TeamService) {
    this.encounter = new Encounter();
    this.selectedEncounter = new Encounter();
    this.selectedTeams = new Array<Team>();
    this.selectedSport = new Sport();
    this.teams = new Array<Team>();
  }

  ngOnInit() {
    this.getEncounters();
    this.getSports();
  }

  public addEncounter(encounter: Encounter) {
    let et = this.makeEncountersTeamsForEncounter();
    encounter.teams = et;
    this.encounterService.addEncounter(encounter)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Encounter successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  private makeEncountersTeamsForEncounter() {
    let et = new Array<EncountersTeams>();
    this.selectedTeams.forEach(team => {
      let t = new EncountersTeams();
      t.team = team;
      t.teamId = parseInt(team.id);
      et.push(t);
    });
    return et;
  }

  private async getEncounters() {
    this.encounters = await this.encounterService.getEncounters();
  }

  private async getSports() {
    this.sports = await this.sportService.getSports();
  }

  onChange(event) {
    this.getTeamsForSport(this.selectedSport.id);
    this.encounter.sportId = this.selectedSport.id;
  }

  private async getTeamsForSport(sportId: string) {
    let t = await this.teamService.getTeams();
    t.forEach(team => {
      if (team.sportId == sportId) {
        this.teams.push(team);
      }
    });
  }

  public updateEncounter(encounter: Encounter) {
    this.encounterService.updateEncounter(encounter)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Encounter successfully updated!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public deleteEncounter(id: string) {
    this.encounterService.deleteEncounter(id)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Encounter successfully deleted!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });;
  }

}
