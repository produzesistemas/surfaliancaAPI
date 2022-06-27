import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PaintManagementComponent } from './paint-management.component';
import { PaintFormComponent } from './paint-form/paint-form.component';
import { PaintFormModule } from './paint-form/paint-form.module';

const routes: Routes = [
    {
        path: '',
        component: PaintManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: PaintFormComponent,
        children: [
            { path: 'paint-form', loadChildren: () => PaintFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PaintManagementRoutingModule { }
