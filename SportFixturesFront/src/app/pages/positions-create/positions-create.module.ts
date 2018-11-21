import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { InputSwitchModule } from 'primeng/inputswitch';
import { DropdownModule } from "primeng/dropdown";
import { PositionsCreateComponent } from '../positions-create/positions-create/positions-create.component';

const routes: Routes = [
    {
        path: '',
        component: PositionsCreateComponent
    },
    {
        path: AppSettings.RouterUrls.resultsManagement,
        component: PositionsCreateComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
        DropdownModule,
        InputSwitchModule
    ],
    declarations: [
        PositionsCreateComponent
    ],
    providers: [

    ],
    exports: [
        PositionsCreateComponent
    ]

})
export class PositionsCreateModule { }