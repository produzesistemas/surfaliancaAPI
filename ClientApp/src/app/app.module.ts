import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthInterceptor } from '../app/_helpers/auth-Interceptor';
import { HttpRequestInterceptor } from '../app/_helpers/http-request.interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import {
  GoogleLoginProvider,
  FacebookLoginProvider,
  AmazonLoginProvider,
} from 'angularx-social-login';

// import { AppLayoutComponent } from './_layouts/app-layout/app-layout.component';
// import { AppHeaderComponent } from './_layouts/app-header/app-header.component';
// import { ClientLayoutComponent } from './_layouts/client-layout/client-layout.component';
// import { ClientHeaderComponent } from './_layouts/client-header/client-header.component';
import { LoginLayoutComponent } from './_layouts/login-layout/login-layout.component';
import { LoginHeaderComponent } from './_layouts/login-header/login-header.component';
// import { TopBarComponent } from './_layouts/top-bar/top-bar.component';
// import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

@NgModule({
  declarations: [
    AppComponent,
    // AppLayoutComponent,
    // AppHeaderComponent,
    // ClientHeaderComponent,
    // ClientLayoutComponent,
    LoginLayoutComponent,
    LoginHeaderComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    NgxSpinnerModule,
    HttpClientModule,
    SocialLoginModule,
    ToastrModule.forRoot(),
    // NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpRequestInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    {      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          { id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('1020904115603-670eedd3h3l3ikeh5l5b49bdidtas3dg.apps.googleusercontent.com'),
          },
          { id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('clientId'),
          },
          { id: AmazonLoginProvider.PROVIDER_ID,
            provider: new AmazonLoginProvider('clientId'),
          },
        ],
      } as SocialAuthServiceConfig ,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
