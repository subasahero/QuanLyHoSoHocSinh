import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GradeCreateEditModalComponent } from './modal/grade-create-edit-modal/grade-create-edit-modal.component';
import { GradesRoutingModule } from './grades-routing.module';

const APP_MODALS = [
  GradeCreateEditModalComponent
];
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    GradesRoutingModule
  ],
  declarations: [
    GradesRoutingModule.components,
    APP_MODALS,
  ],
  providers: [GradesRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class GradesModule { }
