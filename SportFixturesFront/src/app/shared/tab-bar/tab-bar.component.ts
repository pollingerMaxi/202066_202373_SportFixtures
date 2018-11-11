import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { SessionService } from 'src/app/services';

@Component({
  selector: 'app-tab-bar',
  templateUrl: './tab-bar.component.html',
  styleUrls: ['./tab-bar.component.css']
})
export class TabBarComponent implements OnInit {
  public navOptions: MenuItem[];
  public notAuthOptions: MenuItem[];

  constructor(private sessionService: SessionService) { }

  ngOnInit() {
    this.navOptions = [
      { label: 'Home', icon: 'fa fa-stumbleupon-circle', routerLink: 'home' },
      { label: 'Sports Management', icon: 'fa fa-stumbleupon-circle', routerLink: '/sportsManagement' },
      { label: 'Teams Management', icon: 'fa fa-address-card', routerLink: '/teamsManagement' },
      { label: 'Encounters Management', icon: 'fa fa-trophy' },
      { label: 'Users Management', icon: 'fa fa-users' },
      { label: 'Logout', icon: 'fa fa-arrow-circle-right', routerLink: ['/logout'] },
    ];

    this.notAuthOptions = [
      { label: 'Login', icon: 'fa fa-stumbleupon-circle', routerLink: '/login' }
    ];
  }

}
