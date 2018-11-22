import { NgModule } from "@angular/core";
import { HomeComponent } from "./home/home.component";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { TableModule } from "primeng/table";
import { DropdownModule } from "primeng/dropdown";
import { DatePickerModule } from "@syncfusion/ej2-angular-calendars";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

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
        FormsModule,
        TableModule,
        DropdownModule,
        DatePickerModule,
        NgbModule
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