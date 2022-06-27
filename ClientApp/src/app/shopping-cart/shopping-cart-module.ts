import { NgModule } from '@angular/core';
import { ShoppingCartComponent } from './shopping-cart.component';
import { SharedModule } from '../share.module';
import { ShoppingCartRoutingModule} from './shopping-cart-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxViacepModule } from '@brunoc/ngx-viacep';
import { NgxMaskModule, IConfig } from 'ngx-mask';
export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        ShoppingCartRoutingModule,
        NgxViacepModule,
        NgbModule,
        NgxMaskModule.forRoot()
      ],
    declarations: [
        ShoppingCartComponent
    ],
    exports: [ ShoppingCartComponent ]
})
export class ShoppingCartModule { }
