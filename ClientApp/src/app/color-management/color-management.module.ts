import { NgModule } from '@angular/core';
import { ColorManagementComponent } from './color-management.component';
import { SharedModule } from '../share.module';
import { ColorManagementRoutingModule} from './color-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ColorManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        ColorManagementComponent
    ],
    exports: [ ColorManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class ColorManagementModule { }
