import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShoppingCartPaymentComponent } from './shopping-cart-payment/shopping-cart-payment.component';
import { ShoppingCartPaymentModule } from './shopping-cart-payment/shopping-cart-payment.module';
import { ShoppingCartComponent } from './shopping-cart.component';

const routes: Routes = [
    {
        path: '',
        component: ShoppingCartComponent
    },
    {
        path: ':id/:parent',
        component: ShoppingCartPaymentComponent,
        children: [
            { path: '', loadChildren: () => ShoppingCartPaymentModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ShoppingCartRoutingModule { }


