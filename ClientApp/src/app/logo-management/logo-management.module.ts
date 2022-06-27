import { NgModule } from '@angular/core';
import { LogoManagementComponent } from './logo-management.component';
import { SharedModule } from '../share.module';
import { LogoManagementRoutingModule} from './logo-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        LogoManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        LogoManagementComponent
    ],
    exports: [ LogoManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class LogoManagementModule { }
