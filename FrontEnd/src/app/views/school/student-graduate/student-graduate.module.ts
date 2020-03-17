import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentGraduateRoutingModule } from './student-graduate-routing.module';
import { SharedModule } from 'src/app/shared/modules/shared.module';
import { ModalStudentGraduateComponent } from './modals/modal-student-graduate/modal-student-graduate.component';

const APP_MODALS = [
  ModalStudentGraduateComponent
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
