import { NgModule } from '@angular/core';
import { BoardModelManagementComponent } from './board-model-management.component';
import { SharedModule } from '../share.module';
import { BoardModelManagementRoutingModule} from './board-model-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        BoardModelManagementRoutingModule,
        NgbModule,
        NgMultiSelectDropDownModule.forRoot()
      ],
    declarations: [
        BoardModelManagementComponent
    ],
    exports: [ BoardModelManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class BoardModelManagementModule { }
