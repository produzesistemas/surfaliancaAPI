import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BorderColorManagementComponent } from './border-color-management.component';
import { BorderColorFormComponent } from './border-color-form/border-color-form.component';
import { BorderColorFormModule } from './border-color-form/border-color-form.module';

const routes: Routes = [
    {
        path: '',
        component: BorderColorManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: BorderColorFormComponent,
        children: [
            { path: 'border-color-form', loadChildren: () => BorderColorFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BorderColorManagementRoutingModule { }
