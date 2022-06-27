import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ColorManagementComponent } from './color-management.component';
import { ColorFormComponent } from './color-form/color-form.component';
import { ColorFormModule } from './color-form/color-form.module';

const routes: Routes = [
    {
        path: '',
        component: ColorManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: ColorFormComponent,
        children: [
            { path: 'color-form', loadChildren: () => ColorFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ColorManagementRoutingModule { }
