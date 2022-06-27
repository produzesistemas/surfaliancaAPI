import { NgModule } from '@angular/core';
import { ClientLayoutRoutingModule } from './client-layout-routing.module';
import { SharedModule } from '../../share.module';
import {  ClientLayoutComponent } from './client-layout.component';
// import { ClientHeaderComponent } from './../client-header/client-header.component';
// import { FooterBarComponent } from '../../components/footer-bar/footer-bar.component';
// import { ChangePasswordFormComponent } from '../login/change-password-form/change-password-form.component';

@NgModule({
	imports: [
		ClientLayoutRoutingModule,
		SharedModule
	],
	declarations: [
		ClientLayoutComponent
	],
})
export class ClientLayoutModule {}
