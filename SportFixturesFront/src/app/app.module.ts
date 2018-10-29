import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AppSettings } from './config/appSettings';
import { FormsModule } from '@angular/forms';
import { ToasterModule } from 'angular2-toaster';
import { DefaultComponent } from './shared/default/default.component';
import { LoginService, UserService } from './services';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SportsManagementComponent } from './sports-management/sports-management.component';
import { DropdownModule } from 'primeng/dropdown';
import { SportService } from './services/sport.service';

const routes: Routes = [
  {
    path: '',
    component: DefaultComponent
  },
  {
    path: AppSettings.RouterUrls.login,
    component: LoginComponent
  },
  {
    path: AppSettings.RouterUrls.sportsManagement,
    component: SportsManagementComponent
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DefaultComponent,
    PageNotFoundComponent,
    SportsManagementComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ToasterModule.forRoot(),
    BrowserAnimationsModule,
    HttpModule,
    RouterModule.forRoot(routes),
    DropdownModule
  ],
  providers: [LoginService, UserService, SportService],
  bootstrap: [AppComponent]
})
export class AppModule { }
