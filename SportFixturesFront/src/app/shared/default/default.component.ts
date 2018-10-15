import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BaseComponent } from '../base.component';
import { AppSettings } from 'src/app/config/appSettings';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent extends BaseComponent implements OnInit {
  constructor(protected router: Router, protected _route: ActivatedRoute) {
    super(_route);
    this.router.navigateByUrl(AppSettings.RouterUrls.login);
  }

  ngOnInit() {
  }

}
