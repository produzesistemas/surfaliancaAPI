import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BottomManagementComponent } from './bottom-management.component';
import { BottomFormComponent } from './bottom-form/bottom-form.component';
import { BottomFormModule } from './bottom-form/bottom-form.module';

const routes: Routes = [
    {
        path: '',
        component: BottomManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: BottomFormComponent,
        children: [
            { path: 'bottom-form', loadChildren: () => BottomFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BottomManagementRoutingModule { }
