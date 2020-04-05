import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Quảng lý người dùng'
        },
        children: [
            {
                path: 'danh-sach-nguoi-dung',
                component: UserListComponent
            },
            {
                path: '',
                redirectTo: 'danh-sach-nguoi-dung',
                pathMatch: 'full'
            }
        ]
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UserRoutingModule { }
