import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContatoManagementComponent } from './contato-management.component';

const routes: Routes = [
    {
        path: '',
        component: ContatoManagementComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ContatoManagementRoutingModule { }