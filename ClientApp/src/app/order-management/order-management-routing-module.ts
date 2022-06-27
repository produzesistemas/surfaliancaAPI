import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderFormComponent } from './order-form/order-form.component';
import { OrderFormModule } from './order-form/order-form.module';
import { OrderManagementComponent } from './order-management.component';

const routes: Routes = [
    {
        path: '',
        component: OrderManagementComponent
    },
    {
        path: ':id',
        component: OrderFormComponent,
        children: [
            { path: 'order-form', loadChildren: () => OrderFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OrderManagementRoutingModule { }
