import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'src/app/share.module';
import { ShoppingCartPaymentComponent } from './shopping-cart-payment.component';
import { NgxMaskModule, IConfig } from 'ngx-mask';
export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

@NgModule({
    imports: [
      SharedModule,
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      NgbModule,
      NgxMaskModule.forRoot()
      ],
    declarations: [
        ShoppingCartPaymentComponent
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        ShoppingCartPaymentComponent ]
})
export class ShoppingCartPaymentModule { }
