import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppSettings } from '../../config/appSettings';
import { RouterModule, Routes } from '@angular/router';
import { ActionsReportComponent } from './actions-report/actions-report.component';
import { LogsService } from 'src/app/services';
import { DateRangePickerModule } from '@syncfusion/ej2-angular-calendars';
import { FormsModule } from '@angular/forms';

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
        FormsModule
    ],
    declarations: [
        ActionsReportComponent
    ],
    providers: [
        LogsService
    ],
    exports: [
        ActionsReportComponent
    ]

})
export class ActionsReportModule { }