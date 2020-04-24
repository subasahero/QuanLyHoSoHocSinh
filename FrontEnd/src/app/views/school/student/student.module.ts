import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentCreateEditModalComponent } from './modal/student-create-edit-modal/student-create-edit-modal.component';
import { StudentRoutingModule } from './student-routing.module';
import { StudentChangeGradeModalComponent } from './modal/student-change-grade-modal/student-change-grade-modal.component';
import { StudentListEightComponent } from './tab/student-list-eight/student-list-eight.component';
import { StudentListSixComponent } from './tab/student-list-six/student-list-six.component';
import { StudentListSevenComponent } from './tab/student-list-seven/student-list-seven.component';
import { StudentListNineComponent } from './tab/student-list-nine/student-list-nine.component';
import { StudentDinhChiHocModalComponent } from './modal/student-dinh-chi-hoc-modal/student-dinh-chi-hoc-modal.component';

const APP_MODALS = [
  StudentCreateEditModalComponent,
  StudentChangeGradeModalComponent,
  StudentListSixComponent,
  StudentListSevenComponent,
  StudentListEightComponent,
  StudentListNineComponent,
  StudentDinhChiHocModalComponent
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
  providers: [],
  entryComponents: [APP_MODALS]
})
export class StudentModule { }
