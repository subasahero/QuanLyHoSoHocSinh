import { GradeListResolver } from './../../../shared/resolves/grade-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GradeListComponent } from './grade-list/grade-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Lớp'
        },
        children: [
            {
                path: 'danh-sach',
                component: GradeListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                resolve: { 'grade-list': GradeListResolver }
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
export class GradesRoutingModule {
    static components = [
        GradeListComponent
    ];
    static resolvers = [
        GradeListResolver
    ];
}
