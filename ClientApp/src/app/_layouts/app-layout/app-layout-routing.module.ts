import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardMaster } from 'src/app/_guards/auth.guard.master';
import { AppLayoutComponent } from './app-layout.component';

const routes: Routes = [
	{
		path: '',
		component: AppLayoutComponent,
		data: { breadcrumb: null },
		children: [
			{ path: '', redirectTo: 'partner-area' },
			{
				path: 'partner-area',
				loadChildren: () => import('../../partner-area-management/partner-area-management.module').then(m => m.PartnerAreaManagementModule),
                data: { expectedRole: ['Master'], breadcrumb: '' }
			},
			{
				path: 'product',
				loadChildren: () => import('../../product-management/product-management.module').then(m => m.ProductManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Produtos' }
			},
			{
				path: 'construction',
				loadChildren: () => import('../../construction-management/construction-management.module').then(m => m.ConstructionManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Construções' }
			},
			{
				path: 'lamination',
				loadChildren: () => import('../../lamination-management/lamination-management.module').then(m => m.LaminationManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Laminações' }
			},
			{
				path: 'fin-system',
				loadChildren: () => import('../../fin-system-management/fin-system-management.module').then(m => m.FinSystemManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Sistema de quilhas' }
			},
			{
				path: 'tail',
				loadChildren: () => import('../../tail-management/tail-management.module').then(m => m.TailManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Rabetas' }
			},
			{
				path: 'tail-reinforcement',
				loadChildren: () => import('../../tail-reinforcement-management/tail-reinforcement.module').then(m => m.TailReinforcementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Reforço de Rabeta' }
			},
			{
				path: 'bottom',
				loadChildren: () => import('../../bottom-management/bottom-management.module').then(m => m.BottomManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Fundos' }
			},
			{
				path: 'team',
				loadChildren: () => import('../../team-management/team-management.module').then(m => m.TeamManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Equipe' }
			},
			{
				path: 'board-model',
				loadChildren: () => import('../../board-model-management/board-model-management.module').then(m => m.BoardModelManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Modelos' }
			},
			{
				path: 'paint',
				loadChildren: () => import('../../paint-management/paint-management.module').then(m => m.PaintManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Pinturas' }
			},
			{
				path: 'store',
				loadChildren: () => import('../../store-management/store-management.module').then(m => m.StoreManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Loja' }
			},
			{
				path: 'blog',
				loadChildren: () => import('../../blog-management/blog-management.module').then(m => m.BlogManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Blog' }
			},
			{
				path: 'coupon',
				loadChildren: () => import('../../partner-area-coupon-management/partner-area-coupon-management.module').then(m => m.PartnerAreaCouponModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Coupon' }
			},
      {
				path: 'stringer',
				loadChildren: () => import('../../stringer-management/stringer-management.module').then(m => m.StringerManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'stringer' }
			},
      {
				path: 'shipping-company',
				loadChildren: () => import('../../shipping-company-management/shipping-company-management.module').then(m => m.ShippingCompanyManagementModule),
                canActivate: [AuthGuardMaster],
				data: { expectedRole: ['Master'], breadcrumb: 'Transportadora' }
			}

		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class AppLayoutRoutingModule { }
