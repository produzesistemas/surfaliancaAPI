import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientAreaOrderFormComponent } from './client-area-form/client-area-form.component';
import { ClientAreaOrderFormModule } from './client-area-form/client-area-form.module';
import { ClientAreaComponent } from './client-area.component';

const routes: Routes = [
    {
        path: '',
        component: ClientAreaComponent
    },

    {
      path: ':id',
      component: ClientAreaOrderFormComponent,
      children: [
          { path: '', loadChildren: () => ClientAreaOrderFormModule },
        ]
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClientAreaRoutingModule { }
