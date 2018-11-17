import { NgModule } from "@angular/core";
import { HomeComponent } from "./home/home.component";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";

const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: AppSettings.RouterUrls.teamsManagement,
        component: HomeComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule
    ],
    declarations: [
        HomeComponent
    ],
    providers: [

    ],
    exports: [
        HomeComponent
    ]

})
export class HomeModule { }