import { NgModule } from '@angular/core';
import { ExchangePolicyManagementComponent } from './exchange-policy-management.component';
import { SharedModule } from '../share.module';
import { ExchangePolicyManagementRoutingModule} from './exchange-policy-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        ExchangePolicyManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        ExchangePolicyManagementComponent
    ],
    exports: [ ExchangePolicyManagementComponent ]
})
export class ExchangePolicyManagementModule { }
