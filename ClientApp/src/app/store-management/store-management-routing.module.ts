import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StoreManagementComponent } from './store-management.component';

const routes: Routes = [
    {
        path: '',
        component: StoreManagementComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class StoreManagementRoutingModule { }
