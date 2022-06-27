import { NgModule } from '@angular/core';
import { TailManagementComponent } from './tail-management.component';
import { SharedModule } from '../share.module';
import { TailManagementRoutingModule} from './tail-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TailManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        TailManagementComponent
    ],
    exports: [ TailManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class TailManagementModule { }
