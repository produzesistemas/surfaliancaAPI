import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BoardModelManagementComponent } from './board-model-management.component';
import { BoardModelFormComponent } from './board-model-form/board-model-form.component';
import { BoardModelFormModule } from './board-model-form/board-model-form.module';

const routes: Routes = [
    {
        path: '',
        component: BoardModelManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: BoardModelFormComponent,
        children: [
            { path: 'board-model-form', loadChildren: () => BoardModelFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BoardModelManagementRoutingModule { }
