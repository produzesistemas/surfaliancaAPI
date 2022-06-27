import { NgModule } from '@angular/core';
import { BlogManagementComponent } from './blog-management.component';
import { SharedModule } from '../share.module';
import { BlogManagementRoutingModule} from './blog-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        BlogManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        BlogManagementComponent
    ],
    exports: [ BlogManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class BlogManagementModule { }
