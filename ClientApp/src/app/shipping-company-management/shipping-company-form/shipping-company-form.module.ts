import { NgModule } from '@angular/core';
import { ShippingCompanyFormComponent } from './shipping-company-form.component';
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
      ShippingCompanyFormComponent
    ],
    exports: [ ShippingCompanyFormComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class ShippingCompanyFormModule { }
