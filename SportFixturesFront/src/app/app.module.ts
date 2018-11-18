import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AppSettings } from './config/appSettings';
import { FormsModule } from '@angular/forms';
import { ToasterModule } from 'angular2-toaster';
import { DefaultComponent } from './shared/default/default.component';
import { LoginService, UserService, SessionService } from './services';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TabBarComponent } from './shared/tab-bar/tab-bar.component';
import { TabMenuModule } from 'primeng/tabmenu';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AuthenticationGuard } from './shared/guards/authentication.guard';

const routes: Routes = [
  {
    path: '',
    component: DefaultComponent,
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'home',
    component: DefaultComponent,
    canActivate: [AuthenticationGuard]
  },
  {
    path: AppSettings.RouterUrls.login,
    component: LoginComponent
  },
  {
    path: AppSettings.RouterUrls.sportsManagement,
    loadChildren: './pages/sports-management/sports-management.module#SportsManagementModule',
    canActivate: [AuthenticationGuard]
  },
  {
    path: AppSettings.RouterUrls.teamsManagement,
    loadChildren: './pages/teams-management/teams-management.module#TeamsManagementModule',
    canActivate: [AuthenticationGuard]
  },
  {
    path: AppSettings.RouterUrls.actionsReport,
    loadChildren: './pages/actions-report/actions-report.module#ActionsReportModule',
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'logout',
    component: LoginComponent
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
    TabBarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ToasterModule.forRoot(),
    BrowserAnimationsModule,
    HttpModule,
    RouterModule.forRoot(routes),
    
    TabMenuModule,
    AngularFontAwesomeModule
  ],
  providers: [LoginService, UserService, SessionService],
  bootstrap: [AppComponent]
})
export class AppModule { }
