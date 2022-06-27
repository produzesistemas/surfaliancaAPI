import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LaminationManagementComponent } from './lamination-management.component';
import { LaminationFormComponent } from './lamination-form/lamination-form.component';
import { LaminationFormModule } from './lamination-form/lamination-form.module';

const routes: Routes = [
    {
        path: '',
        component: LaminationManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: LaminationFormComponent,
        children: [
            { path: 'lamination-form', loadChildren: () => LaminationFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LaminationManagementRoutingModule { }
