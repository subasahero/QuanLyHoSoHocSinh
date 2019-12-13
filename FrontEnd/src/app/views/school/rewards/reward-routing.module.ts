import { RewardListResolver } from './../../../shared/resolves/reward-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RewardListComponent } from './reward-list/reward-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Khen thưởng'
        },
        children: [
            {
                path: 'danh-sach',
                component: RewardListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                resolve: { 'reward-list': RewardListResolver }
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
export class RewardRoutingModule {
    static components = [
        RewardListComponent
    ];
    static resolvers = [
        RewardListResolver
    ];
}
