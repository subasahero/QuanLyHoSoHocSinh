import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailDisciplineCreateEditModalComponent } from './modal/detail-discipline-create-edit-modal/detail-discipline-create-edit-modal.component';
import { DetailDisciplineRoutingModule } from './detail-discipline-routing.module';

const APP_MODALS = [
  DetailDisciplineCreateEditModalComponent
];
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DetailDisciplineRoutingModule
  ],
  declarations: [
    DetailDisciplineRoutingModule.components,
    APP_MODALS,
  ],
  providers: [DetailDisciplineRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class DetailDisciplineModule { }
