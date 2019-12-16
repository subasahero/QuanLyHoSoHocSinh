import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentScoreListComponent } from './student-score-list/student-score-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Điểm học sinh'
        },
        children: [
            {
                path: 'danh-sach',
                component: StudentScoreListComponent,
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

export class StudentScoreRoutingModule {
    static components = [
        StudentScoreListComponent
    ];
    // static resolvers = [
    //     StudentListResolver
    // ];
}
