import { NgModule } from '@angular/core';
import { AppLayoutRoutingModule } from './app-layout-routing.module';
import { SharedModule } from '../../share.module';
import { AppLayoutComponent } from './app-layout.component';

@NgModule({
	imports: [
		AppLayoutRoutingModule,
		SharedModule
	],
	declarations: [
		AppLayoutComponent
	],
})
export class AppLayoutModule {}
