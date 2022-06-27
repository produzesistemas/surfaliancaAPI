import { NgModule } from '@angular/core';
import { ProductFormComponent } from './product-form.component';
import { SharedModule } from 'src/app/share.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule,
        AngularEditorModule
      ],
    declarations: [
        ProductFormComponent
    ],
    exports: [ ProductFormComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class ProductFormModule { }
