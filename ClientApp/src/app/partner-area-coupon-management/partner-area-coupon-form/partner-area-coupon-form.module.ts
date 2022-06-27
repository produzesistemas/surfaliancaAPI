import { NgModule } from '@angular/core';
import { PartnerAreaCouponFormComponent } from './partner-area-coupon-form.component';
import { SharedModule } from 'src/app/share.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        AngularEditorModule,
        NgbModule
      ],
    declarations: [
        PartnerAreaCouponFormComponent
    ],
    exports: [ PartnerAreaCouponFormComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class PartnerAreaCouponFormModule { }
