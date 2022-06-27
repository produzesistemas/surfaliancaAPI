import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TailManagementComponent } from './tail-management.component';
import { TailFormComponent } from './tail-form/tail-form.component';
import { TailFormModule } from './tail-form/tail-form.module';

const routes: Routes = [
    {
        path: '',
        component: TailManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: TailFormComponent,
        children: [
            { path: 'tail-form', loadChildren: () => TailFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TailManagementRoutingModule { }
