import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Chi tiết'
        },
        children: [
            {
                path: 'ky-luat',
                loadChildren: () => import('./detail-discipline/detail-discipline.module').then(m => m.DetailDisciplineModule)
            },
            {
                path: 'khen-thuong',
                loadChildren: () => import('./detail-rewards/detail-rewards.module').then(m => m.DetailRewardsModule)
            },
            {
                path: '',
                redirectTo: 'khen-thuong',
                pathMatch: 'full'
            }
        ]
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DetailRoutingModule { }
