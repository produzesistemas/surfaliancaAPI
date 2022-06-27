import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'src/app/share.module';
import { ClientAreaOrderFormComponent } from './client-area-form.component';

@NgModule({
    imports: [
      SharedModule,
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      NgbModule
      ],
    declarations: [
      ClientAreaOrderFormComponent
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        ClientAreaOrderFormComponent ]
})
export class ClientAreaOrderFormModule { }
