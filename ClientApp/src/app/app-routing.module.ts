import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexManagementModule } from './index-management/index-management.module';
import { LoginLayoutComponent } from 'src/app/_layouts/login-layout/login-layout.component';
import { LoginAdmManagementModule } from './login-adm-management/login-adm-management.module';
import { OrderManagementModule } from './order-management/order-management.module';
import { ShoppingCartModule } from './shopping-cart/shopping-cart-module';
import { TeamListManagementModule } from './team-list-management/team-list-management.module';
import { AuthGuardClient } from './_guards';
import { AuthGuardMaster } from './_guards/auth.guard.master';
import { PoliticaPrivacidadeModule } from './politicaprivacidade-management/politicaprivacidade-management.module';
import { ContatoManagementModule } from './contato-management/contato-management.module';

const routes: Routes = [
    { path: '', redirectTo: 'index', pathMatch: 'full'},
    { path: 'index', loadChildren: () => IndexManagementModule},
    { path: 'order/:id', loadChildren: () => OrderManagementModule },
    { path: 'shoppingcart', loadChildren: () => ShoppingCartModule },
    { path: 'team-list', loadChildren: () => TeamListManagementModule },
    { path: 'politica-privacidade', loadChildren: () => PoliticaPrivacidadeModule },
    { path: 'contato', loadChildren: () => ContatoManagementModule },

{
  path: '',
  component: LoginLayoutComponent,
  children: [
    { path: 'login-adm', loadChildren: () => LoginAdmManagementModule }
  ]
},
{
  path: 'client-area',
  loadChildren: () => import('./_layouts/client-layout/client-layout.module').then(m => m.ClientLayoutModule),
  canActivate: [AuthGuardClient],
   data: { expectedRole: ['Cliente'] }
},
{
  path: 'partner-area',
  loadChildren: () => import('./_layouts/app-layout/app-layout.module').then(m => m.AppLayoutModule),
  canActivate: [AuthGuardMaster],
   data: { expectedRole: ['Master'] }
}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
