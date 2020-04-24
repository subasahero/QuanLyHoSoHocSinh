import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentQuitListComponent } from './student-quit-list/student-quit-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Học sinh bỏ học'
        },
        children: [
            {
                path: 'danh-sach',
                component: StudentQuitListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                // resolve: { 'student-list': StudentListResolver }
            },
            {
                path: '',
                redirectTo: 'danh-sach',
                pathMatch: 'full'
            }
        ]
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class StudentQuitRoutingModule {
    static components = [
        StudentQuitListComponent
    ];
    // static resolvers = [
    //     StudentListResolver
    // ];
}
