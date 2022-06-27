import { NgModule } from '@angular/core';
import { LaminationFormComponent } from './lamination-form.component';
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
        AngularEditorModule,
        NgbModule
      ],
    declarations: [
        LaminationFormComponent
    ],
    exports: [ LaminationFormComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class LaminationFormModule { }
