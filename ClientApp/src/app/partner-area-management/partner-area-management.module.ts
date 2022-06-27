import { NgModule } from '@angular/core';
import { PartnerAreaManagementComponent } from './partner-area-management.component';
import { SharedModule } from '../share.module';
import { PartnerAreaManagementRoutingModule} from './partner-area-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        PartnerAreaManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        PartnerAreaManagementComponent
    ],
    exports: [ PartnerAreaManagementComponent ]
})
export class PartnerAreaManagementModule { }
