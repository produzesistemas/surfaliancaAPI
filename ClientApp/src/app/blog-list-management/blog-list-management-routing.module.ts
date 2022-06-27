import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogListManagementComponent } from './blog-list-management.component';

const routes: Routes = [
    {
        path: '',
        component: BlogListManagementComponent
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BlogListManagementRoutingModule { }
