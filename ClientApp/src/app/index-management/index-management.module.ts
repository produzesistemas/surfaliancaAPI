import { NgModule } from '@angular/core';
import { IndexManagementComponent } from './index-management.component';
import { SharedModule } from '../share.module';
import { IndexManagementRoutingModule} from './index-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
// import { SafePipe } from '../_services/safepipe.pipe';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        IndexManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        IndexManagementComponent,
        // SafePipe

    ],
    exports: [ IndexManagementComponent ]
})
export class IndexManagementModule { }
