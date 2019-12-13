import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentCreateEditModalComponent } from './modal/student-create-edit-modal/student-create-edit-modal.component';
import { StudentRoutingModule } from './student-routing.module';
import { StudentChangeGradeModalComponent } from './modal/student-change-grade-modal/student-change-grade-modal.component';

const APP_MODALS = [
  StudentCreateEditModalComponent,
  StudentChangeGradeModalComponent
];
@NgModule({
  imports: [
    CommonModule,
    StudentRoutingModule,
    SharedModule
  ],
  declarations: [
    StudentRoutingModule.components,
    APP_MODALS
  ],
  providers: [StudentRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class StudentModule { }
