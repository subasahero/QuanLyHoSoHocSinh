import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/modules/shared.module';
import { UserRoutingModule } from './user-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import { CreateEditUserComponent } from './modals/create-edit-user/create-edit-user.component';

const APP_MODALS = [
  CreateEditUserComponent
];
const APP_COMPONET = [
  UserListComponent
]
@NgModule({
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ],
  declarations: [
    APP_COMPONET,
    APP_MODALS
  ],
  entryComponents: [APP_MODALS]
})
export class UserModule { }
