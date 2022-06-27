import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogoManagementComponent } from './logo-management.component';
import { LogoFormComponent } from './logo-form/logo-form.component';
import { LogoFormModule } from './logo-form/logo-form.module';

const routes: Routes = [
    {
        path: '',
        component: LogoManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: LogoFormComponent,
        children: [
            { path: 'logo-form', loadChildren: () => LogoFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LogoManagementRoutingModule { }
