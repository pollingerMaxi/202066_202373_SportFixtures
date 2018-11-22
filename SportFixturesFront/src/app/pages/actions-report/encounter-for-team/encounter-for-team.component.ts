import { Component, OnInit } from '@angular/core';
import { Team, Encounter } from 'src/app/shared/models';
import { ToasterService } from 'angular2-toaster';
import { TeamService, EncounterService } from 'src/app/services';

@Component({
  selector: 'app-encounter-for-team',
  templateUrl: './encounter-for-team.component.html',
  styleUrls: ['./encounter-for-team.component.css']
})
export class EncounterForTeamComponent implements OnInit {
  public teams: Team[];
  public selectedTeam: Team;
  public encounters: Encounter[];

  constructor(
    private toasterService: ToasterService,
    private teamService: TeamService,
    private encounterService: EncounterService) {
    this.selectedTeam = new Team();
  }

  ngOnInit() {
    this.getSports()
  }

  public async getSports() {
    this.teams = await this.teamService.getTeams();
  }

  async onChange(event) {
    try {
      this.encounters = await this.encounterService.getEncountersOfTeam(this.selectedTeam.id);
    }
    catch (error) {
      this.encounters = [];
      this.toasterService.pop("warning", "Atention!", "No encounters for selected team");
    }
  }
}
