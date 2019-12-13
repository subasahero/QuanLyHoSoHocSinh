import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportPageRoutingModule } from './report-page-routing.module';

const APP_MODALS = [];
@NgModule({
  imports: [
    CommonModule,
    ReportPageRoutingModule,
    SharedModule
  ],
  declarations: [
    ReportPageRoutingModule.components,
    APP_MODALS
  ],
  providers: [ReportPageRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class ReportPageModule { }
