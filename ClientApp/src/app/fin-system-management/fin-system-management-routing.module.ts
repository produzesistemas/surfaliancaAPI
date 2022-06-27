import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FinSystemManagementComponent } from './fin-system-management.component';
import { FinSystemFormComponent } from './fin-system-form/fin-system-form.component';
import { FinSystemFormModule } from './fin-system-form/fin-system-form.module';

const routes: Routes = [
    {
        path: '',
        component: FinSystemManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: FinSystemFormComponent,
        children: [
            { path: 'fin-system-form', loadChildren: () => FinSystemFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FinSystemManagementRoutingModule { }
