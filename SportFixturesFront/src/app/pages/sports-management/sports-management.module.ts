import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SportsManagementComponent } from './sports-management.component';
import { SportService } from '../../services';
import { AppSettings } from '../../config/appSettings';
import { RouterModule, Routes } from '@angular/router';
import { DropdownModule } from 'primeng/dropdown';
import { ListboxModule } from 'primeng/listbox';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
    {
        path: '',
        component: SportsManagementComponent
    },
    {
        path: AppSettings.RouterUrls.sportsManagement,
        component: SportsManagementComponent
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
        SportsManagementComponent
    ],
    providers: [
        
    ],
    exports: [
        SportsManagementComponent
    ]

})
export class SportsManagementModule { }