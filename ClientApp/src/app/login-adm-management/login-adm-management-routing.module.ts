import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginAdmManagementComponent } from './login-adm-management.component';

const routes: Routes = [
    {
        path: '',
        component: LoginAdmManagementComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LoginAdmManagementRoutingModule { }
