import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConstructionManagementComponent } from './construction-management.component';
import { ConstructionFormComponent } from './construction-form/construction-form.component';
import { ConstructionFormModule } from './construction-form/construction-form.module';

const routes: Routes = [
    {
        path: '',
        component: ConstructionManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: ConstructionFormComponent,
        children: [
            { path: 'construction-form', loadChildren: () => ConstructionFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ConstructionManagementRoutingModule { }
