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
  private encounters: Encounter[] = [];
  private comments: Comment[] = [];

  constructor(
    private userService: UserService,
    private sessionService: SessionService,
    private toasterService: ToasterService,
    private encounterService: EncounterService,
    private commentService: CommentService
  ) { }

  async ngOnInit() {
    this.getFavoritesOfUser();
  }

  public getFavoritesOfUser() {
    this.userService.getFavoritesOfUser(this.sessionService.getUser().id)
      .then(async res => {
        this.favorites = JSON.parse(JSON.stringify(res));
        console.log(this.favorites);
        this.getEncountersOfTeam();
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", "Could not get favorites.");
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
          this.toasterService.pop("error", "Error!", error);
        });
    });
  }

  private async getCommentsOfEncounter(encounterId: string) {
    this.comments.concat(await this.commentService.getCommentsOfEncounter(encounterId));
  }
}
