import { NgModule } from '@angular/core';
import { StoreManagementComponent } from './store-management.component';
import { SharedModule } from '../share.module';
import { StoreManagementRoutingModule} from './store-management-routing.module';
import { CommonModule } from '@angular/common';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { NgxViacepModule } from '@brunoc/ngx-viacep';
import { AngularEditorModule } from '@kolkov/angular-editor';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        StoreManagementRoutingModule,
        AngularEditorModule,
        NgxMaskModule.forRoot(options),
        NgxViacepModule
      ],
    declarations: [
        StoreManagementComponent
    ],
    exports: [ StoreManagementComponent ]
})
export class StoreManagementModule { }
