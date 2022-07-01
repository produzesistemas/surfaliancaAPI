import { NgModule } from '@angular/core';
import { TailReinforcementComponent } from './tail-reinforcement.component';
import { SharedModule } from '../share.module';
import { TailReinforcementRoutingModule} from './tail-reinforcement-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TailReinforcementRoutingModule,
        NgbModule
      ],
    declarations: [
        TailReinforcementComponent
    ],
    exports: [ TailReinforcementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class TailReinforcementModule { }
