import { NgModule } from '@angular/core';
import { ProductManagementComponent } from './product-management.component';
import { SharedModule } from '../share.module';
import { ProductManagementRoutingModule} from './product-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ProductManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        ProductManagementComponent
    ],
    exports: [ ProductManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class ProductManagementModule { }
