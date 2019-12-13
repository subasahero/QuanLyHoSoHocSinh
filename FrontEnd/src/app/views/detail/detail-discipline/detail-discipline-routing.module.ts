import { DetailDisciplineListResolver } from './../../../shared/resolves/detail-discipline-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DetailDisciplineListComponent } from './detail-discipline-list/detail-discipline-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Chi tiết khen thưởng'
        },
        children: [
            {
                path: 'danh-sach',
                component: DetailDisciplineListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                resolve: { 'detail-discipline-list': DetailDisciplineListResolver }
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
export class DetailDisciplineRoutingModule {
    static components = [
        DetailDisciplineListComponent
    ];
    static resolvers = [
        DetailDisciplineListResolver
    ];
}
