import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TeamManagementComponent } from './team-management.component';
import { TeamFormComponent } from './team-form/team-form.component';
import { TeamFormModule } from 'src/app/team-management/team-form/team-form.module';

const routes: Routes = [
    {
        path: '',
        component: TeamManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: TeamFormComponent,
        children: [
            { path: 'team-form', loadChildren: () => TeamFormModule },
          ]
      },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TeamManagementRoutingModule { }
