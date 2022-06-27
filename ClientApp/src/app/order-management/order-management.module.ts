import { NgModule } from '@angular/core';
import { OrderManagementComponent } from './order-management.component';
import { SharedModule } from '../share.module';
import { OrderManagementRoutingModule} from './order-management-routing-module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        OrderManagementRoutingModule,
        NgbModule,
      ],
    declarations: [
        OrderManagementComponent
    ],
    exports: [ OrderManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class OrderManagementModule { }
