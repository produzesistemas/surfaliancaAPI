import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShippingCompanyManagementComponent } from './shipping-company-management.component';
import { ShippingCompanyFormComponent } from './shipping-company-form/shipping-company-form.component';
import { ShippingCompanyFormModule } from './shipping-company-form/shipping-company-form.module';

const routes: Routes = [
    {
        path: '',
        component: ShippingCompanyManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: ShippingCompanyFormComponent,
        children: [
            { path: 'shipping-company-form', loadChildren: () => ShippingCompanyFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ShippingCompanyManagementRoutingModule { }
