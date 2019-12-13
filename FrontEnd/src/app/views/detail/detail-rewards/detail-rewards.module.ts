import { SharedModule } from './../../../shared/modules/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailRewardCreateEditModalComponent } from './modal/detail-reward-create-edit-modal/detail-reward-create-edit-modal.component';
import { DetailRewardRoutingModule } from './detail-rewards-routing.module';

const APP_MODALS = [
  DetailRewardCreateEditModalComponent
];
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DetailRewardRoutingModule
  ],
  declarations: [
    DetailRewardRoutingModule.components,
    APP_MODALS,
  ],
  providers: [DetailRewardRoutingModule.resolvers],
  entryComponents: [APP_MODALS]
})
export class DetailRewardsModule { }
