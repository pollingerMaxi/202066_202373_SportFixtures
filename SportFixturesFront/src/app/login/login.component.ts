import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from '../services';
import { LoginModel } from '../shared/models/login';
import { AppSettings } from '../config/appSettings';
import { ToasterService } from 'angular2-toaster';
//import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginParams: LoginModel;

  constructor(private loginService: LoginService,
    private toasterService: ToasterService) { }

  ngOnInit() {
    this.loginParams = new LoginModel();
  }

  public login() {
    this.loginService.login(this.loginParams)
      .subscribe(
        body => {
          this.toasterService.pop("success", "Success!", "User successfully logged in!");
          console.log(body);
          localStorage.setItem(AppSettings.localstorageToken, JSON.stringify(body));
        },
        error => {
          console.log("error");
          this.toasterService.pop("error", "Error!", "Login failed!");
        }
      );
  }

}
