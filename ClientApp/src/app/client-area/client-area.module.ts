import { NgModule } from '@angular/core';
import { ClientAreaComponent } from './client-area.component';
import { SharedModule } from '../share.module';
import { ClientAreaRoutingModule} from './client-area-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        ClientAreaRoutingModule,
        NgbModule,
        FormsModule,
        ReactiveFormsModule,
      ],
    declarations: [
        ClientAreaComponent
    ],
    exports: [ ClientAreaComponent, FormsModule,
      ReactiveFormsModule, ]
})
export class ClientAreaModule { }
