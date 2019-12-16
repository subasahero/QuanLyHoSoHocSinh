import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentScoreRoutingModule } from './student-score-routing.module';
import { TabLevelSixComponent } from './tabs/tab-level-six/tab-level-six.component';
import { TabLevelSevenComponent } from './tabs/tab-level-seven/tab-level-seven.component';
import { TabLevelEightComponent } from './tabs/tab-level-eight/tab-level-eight.component';
import { AddEditStudentScoreModalComponent } from './modals/add-edit-student-score-modal/add-edit-student-score-modal.component';
import { TabLevelNineComponent } from './tabs/tab-level-nine/tab-level-nine.component';

const APP_MODALS = [
  AddEditStudentScoreModalComponent,
  TabLevelSixComponent,
  TabLevelSevenComponent,
  TabLevelEightComponent,
  TabLevelNineComponent,
];
@NgModule({
  imports: [
    CommonModule,
    StudentScoreRoutingModule,
    SharedModule
  ],
  declarations: [
    StudentScoreRoutingModule.components,
    APP_MODALS
  ],
  providers: [],
  entryComponents: [APP_MODALS]
})
export class StudentScoreModule { }
