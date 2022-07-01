import { NgModule } from '@angular/core';
import { TailReinforcementFormComponent } from './tail-reinforcement-form.component';
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
        TailReinforcementFormComponent
    ],
    exports: [ TailReinforcementFormComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class TailReinforcementFormModule { }
