import { NgModule } from '@angular/core';
import { LoginAdmManagementComponent } from './login-adm-management.component';
import { LoginAdmManagementRoutingModule} from './login-adm-management-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';


@NgModule({
    imports: [
        CommonModule,
        LoginAdmManagementRoutingModule,
        ReactiveFormsModule,
        FormsModule,
        ModalModule.forRoot()
      ],
    declarations: [
        LoginAdmManagementComponent    ],
    exports: [ LoginAdmManagementComponent ]
})
export class LoginAdmManagementModule { }

