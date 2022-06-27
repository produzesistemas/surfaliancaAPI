import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BoardTypeManagementComponent } from './board-type-management.component';
import { BoardTypeFormComponent } from './board-type-form/board-type-form.component';
import { BoardTypeFormModule } from './board-type-form/board-type-form.module';

const routes: Routes = [
    {
        path: '',
        component: BoardTypeManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: BoardTypeFormComponent,
        children: [
            { path: 'board-type-form', loadChildren: () => BoardTypeFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BoardTypeManagementRoutingModule { }
