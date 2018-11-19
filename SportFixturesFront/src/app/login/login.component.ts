import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from '../services';
import { LoginModel } from '../shared/models/login';
import { ToasterService } from 'angular2-toaster';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginParams: LoginModel;

  constructor(private loginService: LoginService,
    private toasterService: ToasterService,
    private router: Router,
    private activeRoute: ActivatedRoute) {
    if (activeRoute.snapshot.url[0].path == "logout") {
      this.logout();
    }
  }

  ngOnInit() {
    this.loginParams = new LoginModel();
  }

  public login() {
    this.loginService.login(this.loginParams)
      .subscribe(
        body => {
          this.router.navigate(['/home']);
          this.toasterService.pop("success", "Success!", "User successfully logged in!");
        },
        error => {
          this.toasterService.pop("error", "Error!", "Login failed!");
        }
      );
  }

  public logout() {
    this.loginService.logout();
  }

}
