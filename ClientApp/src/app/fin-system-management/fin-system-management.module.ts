import { NgModule } from '@angular/core';
import { FinSystemManagementComponent } from './fin-system-management.component';
import { SharedModule } from '../share.module';
import { FinSystemManagementRoutingModule} from './fin-system-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FinSystemManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        FinSystemManagementComponent
    ],
    exports: [ FinSystemManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class FinSystemManagementModule { }
