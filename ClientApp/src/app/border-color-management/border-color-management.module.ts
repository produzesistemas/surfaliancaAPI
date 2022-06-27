import { NgModule } from '@angular/core';
import { BorderColorManagementComponent } from './border-color-management.component';
import { SharedModule } from '../share.module';
import { BorderColorManagementRoutingModule} from './border-color-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        BorderColorManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        BorderColorManagementComponent
    ],
    exports: [ BorderColorManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class BorderColorManagementModule { }
