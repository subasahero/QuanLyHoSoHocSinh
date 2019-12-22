import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { DefaultLayoutComponent } from './containers/default-layout/default-layout.component';
import { NgZorroAntdModule, NZ_I18N, vi_VN } from 'ng-zorro-antd';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { registerLocaleData } from '@angular/common';
import vi from '@angular/common/locales/vi';
import { SharedModule } from './shared/modules/shared.module';
import { LoginPageComponent } from './views/login/login-page/login-page.component';
import { EnvServiceProvider } from './env.service.provider';
registerLocaleData(vi);

@NgModule({
  declarations: [
    AppComponent,
    DefaultLayoutComponent,
    LoginPageComponent
  ],
  imports: [
    SharedModule,
    BrowserModule,
    AppRoutingModule,
    NgZorroAntdModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [{ provide: NZ_I18N, useValue: vi_VN },EnvServiceProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
