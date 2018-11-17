import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { UsersManagementComponent } from "./users-management/users-management.component";
import { ListboxModule } from "primeng/listbox";
import { DropdownModule } from "primeng/dropdown";

const routes: Routes = [
    {
        path: '',
        component: UsersManagementComponent
    },
    {
        path: AppSettings.RouterUrls.usersManagement,
        component: UsersManagementComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
        ListboxModule,
        DropdownModule
    ],
    declarations: [
        UsersManagementComponent
    ],
    providers: [

    ],
    exports: [
        UsersManagementComponent
    ]

})
export class UsersModule { }