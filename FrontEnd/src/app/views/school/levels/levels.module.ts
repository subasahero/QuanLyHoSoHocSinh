import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LevelCreateEditModalComponent } from './modal/level-create-edit-modal/level-create-edit-modal.component';
import { LevelsRoutingModule } from './levels-routing.module';

const APP_MODALS = [
  LevelCreateEditModalComponent
];

@NgModule({
  imports: [
    LevelsRoutingModule,
    SharedModule
  ],
  declarations: [
    LevelsRoutingModule.components,
    APP_MODALS
  ],
  providers: [LevelsRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})

export class LevelsModule { }
