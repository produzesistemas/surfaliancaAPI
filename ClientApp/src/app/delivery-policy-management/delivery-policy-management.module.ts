import { NgModule } from '@angular/core';
import { DeliveryPolicyManagementComponent } from './delivery-policy-management.component';
import { SharedModule } from '../share.module';
import { DeliveryPolicyManagementRoutingModule} from './delivery-policy-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        DeliveryPolicyManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        DeliveryPolicyManagementComponent
    ],
    exports: [ DeliveryPolicyManagementComponent ]
})
export class DeliveryPolicyManagementModule { }
