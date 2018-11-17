import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models';
import { UserService, SessionService } from 'src/app/services';
import { PROFILE, USERS_MANAGEMENT } from 'src/app/shared/resources/constStrings';
import { ToasterService } from 'angular2-toaster';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-users-management',
  templateUrl: './users-management.component.html',
  styleUrls: ['./users-management.component.css']
})
export class UsersManagementComponent implements OnInit {
  public users: User[];
  public selectedUser: User;
  public pageTitle: string;
  public user: User;
  public roles: SelectItem[];

  constructor(
    private userService: UserService,
    private toasterService: ToasterService,
    private sessionService: SessionService) {
    this.selectedUser = new User();
    this.user = new User();
  }

  ngOnInit() {
    this.defineTitle();
    this.roles = this.userService.getRoles();
    this.pageTitle === PROFILE ? this.selectedUser = this.sessionService.getUser() : this.getUsers();
  }

  private defineTitle() {
    this.pageTitle = this.sessionService.isAdmin() ? USERS_MANAGEMENT : PROFILE;
  }

  public async getUsers() {
    this.users = await this.userService.getAllUsers();
  }

  public addUser(user: User) {
    this.userService.addUser(user)
      .then(response => {
        this.toasterService.pop("success", "Success!", "User successfully added!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public updateUser(user: User) {
    this.userService.updateUser(user)
      .then(response => {
        this.toasterService.pop("success", "Success!", "User successfully updated!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });
  }

  public deleteUser(id: string) {
    this.userService.deleteUser(id)
      .then(response => {
        this.toasterService.pop("success", "Success!", "User successfully deleted!");
      })
      .catch(error => {
        this.toasterService.pop("error", "Error!", error._body);
      });;
  }
}
