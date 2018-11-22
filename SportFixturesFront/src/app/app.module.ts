import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AppSettings } from './config/appSettings';
import { FormsModule } from '@angular/forms';
import { ToasterModule } from 'angular2-toaster';
import { DefaultComponent } from './shared/default/default.component';
import { LoginService, UserService, SessionService, EncounterService, CommentService, SportService, TeamService, PositionsService } from './services';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { TabBarComponent } from './shared/tab-bar/tab-bar.component';
import { TabMenuModule } from 'primeng/tabmenu';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AuthenticationGuard } from './shared/guards/authentication.guard';
import { AuthorizationGuard } from './shared/guards/authorization.guard';

const routes: Routes = [
  {
    path: AppSettings.RouterUrls.home,
    loadChildren: './pages/home/home.module#HomeModule',
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
    path: AppSettings.RouterUrls.usersManagement,
    loadChildren: './pages/users-management/users.module#UsersModule',
    canActivate: [AuthenticationGuard]
  },
  {
    path: AppSettings.RouterUrls.encountersManagement,
    loadChildren: './pages/encounters-management/encounters-management.module#EncountersManagementModule',
    canActivate: [AuthenticationGuard]
  },
  {
    path: AppSettings.RouterUrls.comments,
    loadChildren: './pages/comments-page/comments.module#CommentsModule',
    canActivate: [AuthenticationGuard]
  },
  {
    path: AppSettings.RouterUrls.actionsReport,
    loadChildren: './pages/actions-report/actions-report.module#ActionsReportModule',
    canActivate: [AuthenticationGuard, AuthorizationGuard]
  },
  {
    path: AppSettings.RouterUrls.positionsTable,
    loadChildren: './pages/positions-table/positions-table.module#PositionsTableModule'
  },
  {
    path: AppSettings.RouterUrls.resultsManagement,
    loadChildren: './pages/positions-create/positions-create.module#PositionsCreateModule',
    canActivate: [AuthorizationGuard]
  },
  {
    path: AppSettings.RouterUrls.fixtureGenerator,
    loadChildren: './pages/fixture-generator/fixture-generator.module#FixtureGeneratorModule',
    canActivate: [AuthenticationGuard, AuthorizationGuard]
  },
  {
    path: 'logout',
    component: LoginComponent
  },
  {
    path: '',
    loadChildren: './pages/home/home.module#HomeModule',
    canActivate: [AuthenticationGuard]
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
  providers: [LoginService, UserService, SessionService, EncounterService, CommentService, SportService, TeamService, PositionsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
