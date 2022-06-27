import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeliveryPolicyManagementComponent } from './delivery-policy-management.component';

const routes: Routes = [
    {
        path: '',
        component: DeliveryPolicyManagementComponent
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DeliveryPolicyManagementRoutingModule { }


