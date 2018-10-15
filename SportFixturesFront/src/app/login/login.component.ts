import { Component, OnInit, Input } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginParams:any = {};

  constructor() { }

  ngOnInit() {
  }

  public login(){
    console.log("method: login")
  }

}
