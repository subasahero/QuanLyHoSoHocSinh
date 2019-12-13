import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RewardCreateEditModalComponent } from './modal/reward-create-edit-modal/reward-create-edit-modal.component';
import { RewardRoutingModule } from './reward-routing.module';

const APP_MODALS = [
  RewardCreateEditModalComponent
];
@NgModule({
  imports: [
    CommonModule,
    RewardRoutingModule,
    SharedModule
  ],
  declarations: [
    RewardRoutingModule.components,
    APP_MODALS
  ],
  providers: [RewardRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class RewardsModule { }
