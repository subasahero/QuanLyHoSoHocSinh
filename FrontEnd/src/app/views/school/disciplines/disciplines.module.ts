import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DisciplineCreateEditModalComponent } from './modal/discipline-create-edit-modal/discipline-create-edit-modal.component';
import { DisciplinesRoutingModule } from './discipline-routing.module';

const APP_MODALS = [
  DisciplineCreateEditModalComponent
]
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DisciplinesRoutingModule
  ],
  declarations: [
    APP_MODALS,
    DisciplinesRoutingModule.components
  ],
  providers: [DisciplinesRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class DisciplinesModule { }
