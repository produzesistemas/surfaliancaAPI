import { NgModule } from '@angular/core';
import { BoardModelDetailComponent } from './board-model-detail.component';
import { SharedModule } from 'src/app/share.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule
      ],
    declarations: [
        BoardModelDetailComponent
    ],
    exports: [ BoardModelDetailComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class BoardModelDetailModule { }
