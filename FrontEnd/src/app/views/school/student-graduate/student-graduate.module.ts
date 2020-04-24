import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentGraduateRoutingModule } from './student-graduate-routing.module';
import { SharedModule } from 'src/app/shared/modules/shared.module';
import { ModalStudentGraduateComponent } from './modals/modal-student-graduate/modal-student-graduate.component';
import { ViewDiemHocSinhTotNghiepComponent } from './modals/view-diem-hoc-sinh-tot-nghiep/view-diem-hoc-sinh-tot-nghiep.component';

const APP_MODALS = [
  ModalStudentGraduateComponent,
  ViewDiemHocSinhTotNghiepComponent
];
@NgModule({
  imports: [
    CommonModule,
    StudentGraduateRoutingModule,
    SharedModule
  ],
  declarations: [
    StudentGraduateRoutingModule.components,
    APP_MODALS
  ],
  providers: [],
  entryComponents: [APP_MODALS]
})
export class StudentModule { }
