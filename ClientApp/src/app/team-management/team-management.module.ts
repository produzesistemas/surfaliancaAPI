import { NgModule } from '@angular/core';
import { TeamManagementComponent } from './team-management.component';
import { SharedModule } from '../share.module';
import { TeamManagementRoutingModule} from './team-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TeamManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        TeamManagementComponent
    ],
    exports: [ TeamManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class TeamManagementModule { }
