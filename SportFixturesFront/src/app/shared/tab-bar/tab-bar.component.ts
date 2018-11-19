import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { SessionService } from 'src/app/services';
import { USERS_MANAGEMENT, PROFILE, SPORTS_MANAGEMENT, TEAMS_MANAGEMENT, HOME, ENCOUNTERS_MANAGEMENT, LOGOUT, LOGIN, REPORTS } from '../resources/constStrings';

@Component({
  selector: 'app-tab-bar',
  templateUrl: './tab-bar.component.html',
  styleUrls: ['./tab-bar.component.css']
})
export class TabBarComponent implements OnInit {
  public adminOptions: MenuItem[];
  public notAuthOptions: MenuItem[];
  public userOptions: MenuItem[];
  public sessionService: SessionService;

  constructor(private sessService: SessionService) { 
    this.sessionService = sessService;
  }

  ngOnInit() {
    this.adminOptions = [
      { label: HOME, icon: 'fa fa-stumbleupon-circle', routerLink: 'home' },
      { label: SPORTS_MANAGEMENT, icon: 'fa fa-stumbleupon-circle', routerLink: '/sportsManagement' },
      { label: TEAMS_MANAGEMENT, icon: 'fa fa-address-card', routerLink: '/teamsManagement' },
      { label: ENCOUNTERS_MANAGEMENT, icon: 'fa fa-trophy' },
      { label: USERS_MANAGEMENT, icon: 'fa fa-users', routerLink: '/usersManagement' },
      { label: REPORTS, icon: 'fa fa-table', routerLink: '/actionsReport' },
      { label: LOGOUT, icon: 'fa fa-arrow-circle-right', routerLink: ['/logout'] }
    ];

    this.userOptions = [
      { label: HOME, icon: 'fa fa-stumbleupon-circle', routerLink: 'home' },
      { label: SPORTS_MANAGEMENT, icon: 'fa fa-stumbleupon-circle', routerLink: '/sportsManagement' },
      { label: TEAMS_MANAGEMENT, icon: 'fa fa-address-card', routerLink: '/teamsManagement' },
      { label: PROFILE, icon: 'fa fa-users', routerLink: '/usersManagement' },
      { label: LOGOUT, icon: 'fa fa-arrow-circle-right', routerLink: ['/logout'] }
    ];

    this.notAuthOptions = [
      { label: LOGIN, icon: 'fa fa-stumbleupon-circle', routerLink: '/login' }
    ];
  }

}
