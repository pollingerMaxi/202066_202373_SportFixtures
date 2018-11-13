import { Component, OnInit } from '@angular/core';
import { UserService, SessionService, EncounterService, CommentService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';
import { Favorite, Encounter, Comment } from 'src/app/shared/models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public favorites: Favorite[];

  constructor(
    private userService: UserService,
    private sessionService: SessionService,
    private toasterService: ToasterService,
    private encounterService: EncounterService,
    private commentService: CommentService
  ) { }

  ngOnInit() {
    this.getFavoritesOfUser();
  }

  public getFavoritesOfUser() {
    this.userService.getFavoritesOfUser(this.sessionService.getUser().id)
      .then(res => {
        this.favorites = JSON.parse(JSON.stringify(res));
        console.log(this.favorites);
        this.getEncountersOfTeam();
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", "Could not get favorites.");
      });
  }

  public async getEncountersOfTeam() {
    let encounters: Encounter[] = [];
    this.favorites.forEach(async fav => {
      try {
        await this.encounterService.getEncountersOfTeam(fav.teamId)
      }
      catch (error) {
        this.toasterService.pop("error", "Error!", error);
      }
      encounters.concat();
    });
  }

  public getCommentsOfEncounter() {
    let comments: Comment[] = [];

  }

}
