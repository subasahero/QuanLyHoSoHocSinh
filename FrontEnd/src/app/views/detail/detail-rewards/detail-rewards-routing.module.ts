import { DetailRewardListResolver } from './../../../shared/resolves/detail-reward-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DetailRewardsListComponent } from './detail-rewards-list/detail-rewards-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Chi tiết khen thưởng'
        },
        children: [
            {
                path: 'danh-sach',
                component: DetailRewardsListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                resolve: { 'detail-reward-list': DetailRewardListResolver }
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
export class DetailRewardRoutingModule {
    static components = [
        DetailRewardsListComponent
    ];
    static resolvers = [
        DetailRewardListResolver
    ];
}
