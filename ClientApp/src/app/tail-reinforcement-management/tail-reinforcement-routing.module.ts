import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TailReinforcementComponent } from './tail-reinforcement.component';
import { TailReinforcementFormComponent } from './tail-reinforcement-form/tail-reinforcement-form.component';
import { TailReinforcementFormModule } from './tail-reinforcement-form/tail-reinforcement-form.module';

const routes: Routes = [
    {
        path: '',
        component: TailReinforcementComponent
    },
    {
        path: ':id/:isEdit',
        component: TailReinforcementFormComponent,
        children: [
            { path: 'tail-reinforcement-form', loadChildren: () => TailReinforcementFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TailReinforcementRoutingModule { }
