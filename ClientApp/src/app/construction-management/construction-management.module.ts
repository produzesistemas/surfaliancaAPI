import { NgModule } from '@angular/core';
import { ConstructionManagementComponent } from './construction-management.component';
import { SharedModule } from '../share.module';
import { ConstructionManagementRoutingModule} from './construction-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ConstructionManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        ConstructionManagementComponent
    ],
    exports: [ ConstructionManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class ConstructionManagementModule { }
