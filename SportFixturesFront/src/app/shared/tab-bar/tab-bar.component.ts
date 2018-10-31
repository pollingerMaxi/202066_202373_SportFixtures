import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-tab-bar',
  templateUrl: './tab-bar.component.html',
  styleUrls: ['./tab-bar.component.css']
})
export class TabBarComponent implements OnInit {
  public navOptions: MenuItem[];

  constructor() { }

  ngOnInit() {
    this.navOptions = [
      { label: 'Sports Management', icon: 'fa fa-stumbleupon-circle' },
      { label: 'Teams Management', icon: 'fa fa-address-card' },
      { label: 'Encounters Management', icon: 'fa fa-trophy' },
      { label: 'Users Management', icon: 'fa fa-users' }
    ];
  }

}
