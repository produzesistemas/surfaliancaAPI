import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StringerManagementComponent } from './stringer-management.component';
import { StringerFormComponent } from './stringer-form/stringer-form.component';
import { StringerFormModule } from './stringer-form/stringer-form.module';

const routes: Routes = [
    {
        path: '',
        component: StringerManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: StringerFormComponent,
        children: [
            { path: 'stringer-form', loadChildren: () => StringerFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class StringerManagementRoutingModule { }
