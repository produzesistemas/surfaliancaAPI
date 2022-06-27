import { NgModule } from '@angular/core';
import { OrderFormComponent } from './order-form.component';
import { SharedModule } from 'src/app/share.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule
      ],
    declarations: [
        OrderFormComponent
    ],
    exports: [ OrderFormComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class OrderFormModule { }
