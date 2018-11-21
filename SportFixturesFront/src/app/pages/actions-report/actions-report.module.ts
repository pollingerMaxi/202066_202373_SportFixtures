import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppSettings } from '../../config/appSettings';
import { RouterModule, Routes } from '@angular/router';
import { ActionsReportComponent } from './actions-report/actions-report.component';
import { LogsService } from 'src/app/services';
import { DateRangePickerModule } from '@syncfusion/ej2-angular-calendars';
import { FormsModule } from '@angular/forms';
import { EncounterForSportComponent } from './encounters-for-sport/encounter-for-sport.component';
import { ListboxModule } from 'primeng/listbox';
import { DropdownModule } from 'primeng/dropdown';

const routes: Routes = [
    {
        path: '',
        component: ActionsReportComponent
    },
    {
        path: AppSettings.RouterUrls.sportsManagement,
        component: ActionsReportComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        DateRangePickerModule,
        FormsModule,
        ListboxModule,
        DropdownModule
    ],
    declarations: [
        ActionsReportComponent,
        EncounterForSportComponent
    ],
    providers: [
        LogsService
    ],
    exports: [
        ActionsReportComponent
    ]

})
export class ActionsReportModule { }