import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReportListComponent } from './report-list/report-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Báo cáo'
        },
        children: [
            {
                path: '',
                component: ReportListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                // resolve: { 'student-list': StudentListResolver }
            },
            {
                path: '',
                redirectTo: '',
                pathMatch: 'full'
            }
        ]
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class ReportPageRoutingModule {
    static components = [
        ReportListComponent
    ];
    static resolvers = [
        // StudentListResolver
    ];
}
