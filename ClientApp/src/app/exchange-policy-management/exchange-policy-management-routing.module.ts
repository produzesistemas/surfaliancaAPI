import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExchangePolicyManagementComponent } from './exchange-policy-management.component';

const routes: Routes = [
    {
        path: '',
        component: ExchangePolicyManagementComponent
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ExchangePolicyManagementRoutingModule { }


