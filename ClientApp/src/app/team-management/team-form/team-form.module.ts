import { NgModule } from '@angular/core';
import { TeamFormComponent } from './team-form.component';
import { SharedModule } from 'src/app/share.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule,
        AngularEditorModule
      ],
    declarations: [
        TeamFormComponent
    ],
    exports: [ TeamFormComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class TeamFormModule { }
