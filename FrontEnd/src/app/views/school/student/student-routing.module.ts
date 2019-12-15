import { StudentListResolver } from './../../../shared/resolves/student-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentListComponent } from './student-list/student-list.component';
import { StudentListSevenComponent } from './tab/student-list-seven/student-list-seven.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Học sinh'
        },
        children: [
            {
                path: 'danh-sach',
                component: StudentListComponent,
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

export class StudentRoutingModule {
    static components = [
        StudentListComponent
    ];
    // static resolvers = [
    //     StudentListResolver
    // ];
}
