import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { DropdownModule } from "primeng/dropdown";
import { ListboxModule } from "primeng/listbox";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { NgModule } from "@angular/core";
import { EncountersManagementComponent } from "./encounters-management/encounters-management.component";

const routes: Routes = [
    {
        path: '',
        component: EncountersManagementComponent
    },
    {
        path: AppSettings.RouterUrls.encountersManagement,
        component: EncountersManagementComponent
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
        EncountersManagementComponent
    ],
    providers: [
        
    ],
    exports: [
        EncountersManagementComponent
    ]

})
export class EncountersManagementModule { }