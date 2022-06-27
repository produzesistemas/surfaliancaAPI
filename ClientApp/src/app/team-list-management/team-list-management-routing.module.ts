import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TeamListManagementComponent } from './team-list-management.component';

const routes: Routes = [
    {
        path: '',
        component: TeamListManagementComponent
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TeamListManagementRoutingModule { }
