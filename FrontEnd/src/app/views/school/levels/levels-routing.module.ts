import { LevelListResolver } from './../../../shared/resolves/level-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LevelListComponent } from './level-list/level-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Khối'
        },
        children: [
            {
                path: 'danh-sach',
                component: LevelListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                resolve: { 'level-list': LevelListResolver }
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
export class LevelsRoutingModule {
    static components = [
        LevelListComponent
    ];
    static resolvers = [
        LevelListResolver
    ];
}
