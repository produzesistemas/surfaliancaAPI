import { NgModule } from '@angular/core';
import { PaintManagementComponent } from './paint-management.component';
import { SharedModule } from '../share.module';
import { PaintManagementRoutingModule} from './paint-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        PaintManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        PaintManagementComponent
    ],
    exports: [ PaintManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class PaintManagementModule { }
