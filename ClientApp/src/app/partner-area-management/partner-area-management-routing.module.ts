import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PartnerAreaManagementComponent } from './partner-area-management.component';

const routes: Routes = [
    {
        path: '',
        component: PartnerAreaManagementComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PartnerAreaManagementRoutingModule { }
