import { NgModule } from '@angular/core';
import { ShippingCompanyManagementComponent } from './shipping-company-management.component';
import { SharedModule } from '../share.module';
import { ShippingCompanyManagementRoutingModule} from './shipping-company-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ShippingCompanyManagementRoutingModule,
        NgbModule
      ],
    declarations: [
      ShippingCompanyManagementComponent
    ],
    exports: [ ShippingCompanyManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class ShippingCompanyManagementModule { }
