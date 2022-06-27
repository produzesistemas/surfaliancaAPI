import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductManagementComponent } from './product-management.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductFormModule } from './product-form/product-form.module';

const routes: Routes = [
    {
        path: '',
        component: ProductManagementComponent
    },
    {
        path: ':id',
        component: ProductFormComponent,
        children: [
            { path: 'product-form', loadChildren: () => ProductFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductManagementRoutingModule { }
