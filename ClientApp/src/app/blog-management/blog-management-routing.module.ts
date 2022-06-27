import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogManagementComponent } from './blog-management.component';
import { BlogFormComponent } from './blog-form/blog-form.component';
import { BlogFormModule } from './blog-form/blog-form.module';

const routes: Routes = [
    {
        path: '',
        component: BlogManagementComponent
    },
    {
        path: ':id/:isEdit',
        component: BlogFormComponent,
        children: [
            { path: 'blog-form', loadChildren: () => BlogFormModule },
          ]
      },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BlogManagementRoutingModule { }
