import { TeamsManagementComponent } from "./teams-management.component";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { DropdownModule } from "primeng/dropdown";
import { ListboxModule } from "primeng/listbox";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { NgModule } from "@angular/core";
import { SportService, TeamService, SessionService } from "src/app/services";

const routes: Routes = [
    {
        path: '',
        component: TeamsManagementComponent
    },
    {
        path: AppSettings.RouterUrls.teamsManagement,
        component: TeamsManagementComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        DropdownModule,
        ListboxModule,
        FormsModule
    ],
    declarations: [
        TeamsManagementComponent
    ],
    providers: [
        SportService, TeamService, SessionService
    ],
    exports: [
        TeamsManagementComponent
    ]

})
export class TeamsManagementModule { }