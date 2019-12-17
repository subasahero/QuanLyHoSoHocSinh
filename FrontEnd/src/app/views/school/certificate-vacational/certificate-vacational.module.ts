import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CertificateVacationalRoutingModule } from './certificate-vacational-routing.module';

const APP_MODALS = [
];
@NgModule({
  imports: [
    CommonModule,
    CertificateVacationalRoutingModule,
    SharedModule
  ],
  declarations: [
    CertificateVacationalRoutingModule.components,
    APP_MODALS
  ],
  providers: [CertificateVacationalRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class CertificateVacationalModule { }
