import { Component, OnInit } from '@angular/core';
import { UserService, SessionService } from 'src/app/services';
import { ToasterService } from 'angular2-toaster';
import { Favorite } from 'src/app/shared/models';

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
    private toasterService: ToasterService
  ) { }

  ngOnInit() {
    this.getFavoritesOfUser();
  }

  public getFavoritesOfUser() {
    this.userService.getFavoritesOfUser(this.sessionService.getUser().id)
      .then(res => {
        this.favorites = JSON.parse(JSON.stringify(res));
        console.log(this.favorites);
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", "Could not get favorites.");
      });
  }

}
