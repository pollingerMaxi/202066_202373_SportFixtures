import { Component, OnInit } from '@angular/core';
import { Team, Sport, Favorite, Order } from 'src/app/shared/models';
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
  public order: boolean = true;

  constructor(
    private teamService: TeamService,
    private toasterService: ToasterService,
    public sessionService: SessionService,
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
    team.sportId = this.selectedSport.id;
    this.teamService.addTeam(team)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Team successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public updateTeam(team: Team) {
    this.teamService.updateTeam(team)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Team successfully updated!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public deleteTeam(id: string) {
    this.teamService.deleteTeam(id)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Team successfully deleted!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });;
  }

  public async getSport() {
    this.selectedTeamSport = await this.sportService.getSportById(this.selectedTeam.sportId);
    this.srcData = this.sanitizer.bypassSecurityTrustResourceUrl("data:image/png;base64," + this.selectedTeam.photo);
  }

  public async getSports() {
    this.sports = await this.sportService.getSports();
  }

  public onUpload(event) {
    let file = event.target.files;
    var reader = new FileReader();
    reader.onload = this._handleReaderLoaded.bind(this);
    reader.readAsBinaryString(file[0]);
  }

  private _handleReaderLoaded(readerEvt) {
    var binaryString = readerEvt.target.result;
    this.selectedTeam.photo = btoa(binaryString);
  }

  public followTeam(team: Team) {
    let fav = new Favorite();
    fav.teamId = team.id;
    fav.userId = this.sessionService.getUser().id;
    this.teamService.followTeam(fav)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Now following team: " + team.name);
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  async onChange(event) {
    if (event.checked) {
      this.teams = await this.teamService.getTeamsFiltered(Order.Ascending);
    } else {
      this.teams = await this.teamService.getTeamsFiltered(Order.Descending);
    }
  }

  public unfollowTeam(team: Team) {
    let fav = new Favorite();
    fav.teamId = team.id;
    fav.userId = this.sessionService.getUser().id;
    this.teamService.unfollowTeam(fav)
      .then(response => {
        this.toasterService.pop("success", "Success!", "No longer following: " + team.name);
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

}
