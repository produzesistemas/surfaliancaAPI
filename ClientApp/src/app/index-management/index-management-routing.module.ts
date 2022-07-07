import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexDetailProductComponent } from './index-detail-product/index-detail-product.component';
import { IndexDetailProductModule } from './index-detail-product/index-detail-product.module';
import { IndexManagementComponent } from './index-management.component';

const routes: Routes = [
    {
        path: '',
        component: IndexManagementComponent
    },
    {
        path: ':id',
        component: IndexDetailProductComponent,
        children: [
            { path: '', loadChildren: () => IndexDetailProductModule },
          ]
      },
    //   {
    //     path: 'boardModel',
    //     component: BoardModelDetailComponent,
    //     children: [
    //         { path: '', loadChildren: () => BoardModelDetailModule },
    //       ]
    //   },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class IndexManagementRoutingModule { }
