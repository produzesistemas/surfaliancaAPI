import { NgModule } from '@angular/core';
import { BoardTypeManagementComponent } from './board-type-management.component';
import { SharedModule } from '../share.module';
import { BoardTypeManagementRoutingModule} from './board-type-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        BoardTypeManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        BoardTypeManagementComponent
    ],
    exports: [ BoardTypeManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class BoardTypeManagementModule { }
