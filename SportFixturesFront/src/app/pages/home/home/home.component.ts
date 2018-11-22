import { Component, OnInit } from '@angular/core';
import { UserService, SessionService, EncounterService, CommentService, SportService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';
import { Favorite, Encounter, Comment, Sport } from 'src/app/shared/models';
import * as moment from 'moment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public dateFormat: String = "dd-MM-yyyy";
  public favorites: Favorite[];
  public encounters: Encounter[] = [];
  public comments: Comment[] = [];
  public sports: Sport[] = [];
  public selectedSport: Sport;
  public filteredEncounters: Encounter[] = [];

  constructor(
    private userService: UserService,
    private sessionService: SessionService,
    private toasterService: ToasterService,
    private encounterService: EncounterService,
    private commentService: CommentService,
    private sportService: SportService) {
    this.selectedSport = new Sport();
  }

  ngOnInit() {
    this.getFavoritesOfUser();
    this.getSports();
  }

  public getFavoritesOfUser() {
    this.userService.getFavoritesOfUser(this.sessionService.getUser().id)
      .then(async res => {
        this.favorites = JSON.parse(JSON.stringify(res));
        this.getEncountersOfTeam();
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public getEncountersOfTeam() {
    this.favorites.forEach(fav => {
      this.encounterService.getEncountersOfTeam(fav.teamId)
        .then(encs => {
          encs.forEach(async enc => {
            await this.getCommentsOfEncounter(enc.id);
          });
        })
        .catch(error => {
          this.toasterService.pop("error", "Error!", error._body);
        });
    });
  }

  private async getCommentsOfEncounter(encounterId: string) {
    let obtained = await this.commentService.getCommentsOfEncounter(encounterId);
    this.comments = this.comments.concat(obtained);
    this.comments.sort(function
      (first, second) {
      return parseInt(first.encounterId) - parseInt(second.encounterId)
    });
  }

  private async getSports() {
    this.sports = await this.sportService.getSports();
  }

  public getEncountersOfSport() {
    this.encounterService.getEncountersOfSport(this.selectedSport.id)
      .then(encs => {
        this.encounters = encs;
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public updateEncounters(date) {
    this.filteredEncounters = this.encounters
      .filter(e =>
        moment(e.date).format("L") == moment(date).format("L")
      );
  }

}

