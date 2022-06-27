import { NgModule } from '@angular/core';
import { BlogListManagementComponent } from './blog-list-management.component';
import { SharedModule } from '../share.module';
import { BlogListManagementRoutingModule} from './blog-list-management-routing.module';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        BlogListManagementRoutingModule,
        NgbModule
      ],
    declarations: [
        BlogListManagementComponent
    ],
    exports: [ BlogListManagementComponent,
        FormsModule,
        ReactiveFormsModule ]
})
export class BlogListManagementModule { }
