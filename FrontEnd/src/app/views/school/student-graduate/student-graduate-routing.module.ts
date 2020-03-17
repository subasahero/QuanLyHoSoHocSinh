import { StudentListResolver } from './../../../shared/resolves/student-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentGraduateListComponent } from './student-graduate-list/student-graduate-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Học sinh tốt nghiệp'
        },
        children: [
            {
                path: 'danh-sach',
                component: StudentGraduateListComponent,
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

export class StudentGraduateRoutingModule {
    static components = [
        StudentGraduateListComponent
    ];
    // static resolvers = [
    //     StudentListResolver
    // ];
}
