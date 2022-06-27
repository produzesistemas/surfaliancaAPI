import { NgModule } from '@angular/core';
import { ContatoManagementComponent } from './contato-management.component';
import { SharedModule } from '../share.module';
import { ContatoManagementRoutingModule} from './contato-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxMaskModule, IConfig } from 'ngx-mask';
export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;


@NgModule({
  imports: [
      SharedModule,
      CommonModule,
      ContatoManagementRoutingModule,
      NgbModule,
      NgxMaskModule.forRoot()
    ],
  declarations: [
    ContatoManagementComponent
  ],
  exports: [ ContatoManagementComponent ]
})
export class ContatoManagementModule { }