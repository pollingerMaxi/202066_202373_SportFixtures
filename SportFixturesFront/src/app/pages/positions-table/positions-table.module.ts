import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { PositionsTableComponent } from "./positions-table/positions-table.component";
import { TableModule } from 'primeng/table';
import { PositionsService } from "src/app/services";

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
        TableModule
    ],
    declarations: [
        PositionsTableComponent
    ],
    providers: [
        PositionsService
    ],
    exports: [
        PositionsTableComponent
    ]

})
export class PositionsTableModule { }