import { Component, OnInit } from '@angular/core';
import { Encounter, Comment } from 'src/app/shared/models';
import { CommentService, EncounterService, SessionService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';

@Component({
  selector: 'app-comments-page',
  templateUrl: './comments-page.component.html',
  styleUrls: ['./comments-page.component.css']
})
export class CommentsPageComponent implements OnInit {
  public encounters: Encounter[];
  public selectedEncounter: Encounter;
  public comment: Comment;

  constructor(
    private commentService: CommentService,
    private toasterService: ToasterService,
    private encounterService: EncounterService,
    private sessionService: SessionService) {
    this.selectedEncounter = new Encounter();
    this.comment = new Comment();
  }

  ngOnInit() {
    this.getEncounters();
  }

  public async getEncounters() {
    this.encounters = await this.encounterService.getEncounters();
  }

  public addComment(comment: Comment) {
    comment.userId = this.sessionService.getUser().id;
    comment.encounterId = this.selectedEncounter.id;
    this.commentService.addComment(comment)
      .then(response => {
        this.toasterService.pop("success", "Success!", "Comment successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

}
