import { Component, OnInit } from '@angular/core';
import { Team, Sport } from 'src/app/shared/models';
import { SessionService, TeamService, SportService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-teams-management',
  templateUrl: './teams-management.component.html',
  styleUrls: ['./teams-management.component.css']
})
export class TeamsManagementComponent implements OnInit {
  public team: Team;
  public teams: Team[];
  public selectedTeam: Team;
  public selectedTeamSport: Sport;
  public srcData: SafeResourceUrl;
  public sports: Sport[];
  public selectedSport: Sport;
  public uploadedFiles: any[] = [];

  constructor(
    private teamService: TeamService,
    private toasterService: ToasterService,
    private sessionService: SessionService,
    private sportService: SportService,
    private sanitizer: DomSanitizer) {
    this.team = new Team();
    this.selectedTeam = new Team();
    this.selectedTeamSport = new Sport();
    this.selectedSport = new Sport();
  }

  ngOnInit() {
    this.getTeams();
    this.getSports();
  }

  public async getTeams() {
    this.teams = await this.teamService.getTeams();
  }

  public addTeam(team: Team) {
    this.teamService.addTeam(team)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Team successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", "Could not add team.");
      });
  }

  public updateTeam(team: Team) {
    this.teamService.updateTeam(team)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Team successfully updated!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", "Could not update team.");
      });
  }

  public deleteTeam(id: string) {
    this.teamService.deleteTeam(id)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Team successfully updated!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", "Could not update team.");
      });;
  }

  public async getSport() {
    this.selectedTeamSport = await this.sportService.getSportById(this.selectedTeam.sportId);
    this.srcData = this.sanitizer.bypassSecurityTrustResourceUrl("data:image/png;base64," + this.selectedTeam.photoPath);
  }

  public async getSports() {
    this.sports = await this.sportService.getSports();
  }

  public onUpload(event) {
    for (let file of event.files) {
      this.uploadedFiles.push(file);
    }
  }

}
