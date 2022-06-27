import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'src/app/share.module';
import { IndexDetailProductComponent } from './index-detail-product.component';

@NgModule({
    imports: [
      SharedModule,
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      NgbModule
      ],
    declarations: [
      IndexDetailProductComponent
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        IndexDetailProductComponent ]
})
export class IndexDetailProductModule { }
