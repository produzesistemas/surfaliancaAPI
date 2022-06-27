import { NgModule } from '@angular/core';
import { LaminationManagementComponent } from './lamination-management.component';
import { SharedModule } from '../share.module';
import { LaminationManagementRoutingModule} from './lamination-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        LaminationManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        LaminationManagementComponent
    ],
    exports: [ LaminationManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class LaminationManagementModule { }
