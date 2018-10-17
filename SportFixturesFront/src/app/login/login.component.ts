import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from '../services';
import { LoginModel } from '../shared/models/login';
import { AppSettings } from '../config/appSettings';
//import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginParams: LoginModel;

  constructor(private loginService: LoginService) { }

  ngOnInit() {
    this.loginParams = new LoginModel();
  }

  public login() {
    console.log("method: login")
    this.loginService.login(this.loginParams)
      .subscribe(
        body => {
          console.log("logged in");
          console.log(body);
          localStorage.setItem(AppSettings.localstorageToken, JSON.stringify(body));
        },
        error => {
          console.log("error");
        }
      )
  }

}
