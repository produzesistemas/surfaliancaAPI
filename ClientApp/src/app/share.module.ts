import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule, BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { RouterModule } from '@angular/router';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CommonModule, registerLocaleData, CurrencyPipe } from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import { TopBarComponent } from './_layouts/top-bar/top-bar.component'
import { TopBarClientComponent } from './_layouts/top-bar-client/top-bar-client.component'
import { TopBarMasterComponent } from './_layouts/top-bar-master/top-bar-master.component'
registerLocaleData(ptBr);
import { SafePipe } from '../app/_services/safepipe.pipe';


@NgModule({
  imports: [
    RouterModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    CurrencyMaskModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),

  ],
  declarations: [
    TopBarComponent, TopBarClientComponent, TopBarMasterComponent, SafePipe
  ],
  providers: [
    CurrencyPipe,
    BsModalService,
    BsModalRef,
    { provide: LOCALE_ID, useValue: 'pt' }
  ],
  entryComponents: [],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CurrencyMaskModule,
    TopBarComponent,
    TopBarClientComponent,
    TopBarMasterComponent, SafePipe
  ]
})
export class SharedModule { }
