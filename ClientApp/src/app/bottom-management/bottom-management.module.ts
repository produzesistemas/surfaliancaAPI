import { NgModule } from '@angular/core';
import { BottomManagementComponent } from './bottom-management.component';
import { SharedModule } from '../share.module';
import { BottomManagementRoutingModule} from './bottom-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        BottomManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        BottomManagementComponent
    ],
    exports: [ BottomManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class BottomManagementModule { }
