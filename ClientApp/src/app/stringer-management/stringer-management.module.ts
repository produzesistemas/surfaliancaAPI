import { NgModule } from '@angular/core';
import { StringerManagementComponent } from './stringer-management.component';
import { SharedModule } from '../share.module';
import { StringerManagementRoutingModule} from './stringer-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        StringerManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        StringerManagementComponent
    ],
    exports: [ StringerManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class StringerManagementModule { }
