import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { PositionsTableComponent } from "./positions-table/positions-table.component";
import { TableModule } from 'primeng/table';
import { DropdownModule } from "primeng/dropdown";

const routes: Routes = [
    {
        path: '',
        component: PositionsTableComponent
    },
    {
        path: AppSettings.RouterUrls.positionsTable,
        component: PositionsTableComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
        TableModule,
        DropdownModule
    ],
    declarations: [
        PositionsTableComponent
    ],
    providers: [

    ],
    exports: [
        PositionsTableComponent
    ]

})
export class PositionsTableModule { }