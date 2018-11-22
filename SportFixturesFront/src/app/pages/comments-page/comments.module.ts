import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AppSettings } from "src/app/config/appSettings";
import { DropdownModule } from "primeng/dropdown";
import { CommentsPageComponent } from "./comments-page/comments-page.component";
import { InputTextareaModule } from 'primeng/inputtextarea';

const routes: Routes = [
    {
        path: '',
        component: CommentsPageComponent
    },
    {
        path: AppSettings.RouterUrls.comments,
        component: CommentsPageComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
        DropdownModule,
        InputTextareaModule
    ],
    declarations: [
        CommentsPageComponent
    ],
    providers: [

    ],
    exports: [
        CommentsPageComponent
    ]

})
export class CommentsModule { }