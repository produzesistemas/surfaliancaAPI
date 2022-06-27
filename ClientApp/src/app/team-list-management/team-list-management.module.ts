import { NgModule } from '@angular/core';
import { TeamListManagementComponent } from './team-list-management.component';
import { SharedModule } from '../share.module';
import { TeamListManagementRoutingModule} from './team-list-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TeamListManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        TeamListManagementComponent
    ],
    exports: [ TeamListManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class TeamListManagementModule { }
