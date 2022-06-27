import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientLayoutComponent } from './client-layout.component';

const routes: Routes = [
    
	{
		path: '',
		component: ClientLayoutComponent,
		data: { breadcrumb: null },
		children: [
			{ path: '', redirectTo: 'client-area' },
			{
				path: 'order',
				loadChildren: () => import('../../client-area/client-area.module').then(m => m.ClientAreaModule),
				data: { expectedRole: ['Cliente'], breadcrumb: '' }
			},
		
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class ClientLayoutRoutingModule { }
