import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentQuitRoutingModule } from './student-quit-routing.module';
import { SharedModule } from 'src/app/shared/modules/shared.module';
import { ViewDiemHocSinhBoHocComponent } from './modals/view-diem-hoc-sinh-bo-hoc/view-diem-hoc-sinh-bo-hoc.component';
import { ViewHocSinhBoHocComponent } from './modals/view-hoc-sinh-bo-hoc/view-hoc-sinh-bo-hoc.component';

const APP_MODALS = [
  ViewDiemHocSinhBoHocComponent,
  ViewHocSinhBoHocComponent
];
@NgModule({
  imports: [
    CommonModule,
    StudentQuitRoutingModule,
    SharedModule
  ],
  declarations: [
    StudentQuitRoutingModule.components,
    APP_MODALS
  ],
  providers: [],
  entryComponents: [APP_MODALS]
})
export class StudentQuitModule { }
