import { DisciplineListResolver } from './../../../shared/resolves/discipline-list.resolver';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { DisciplineListComponent } from './discipline-list/discipline-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Kỷ luật'
        },
        children: [
            {
                path: 'danh-sach',
                component: DisciplineListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                resolve: { 'discipline-list': DisciplineListResolver }
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
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
  ],
})
export class DisciplinesRoutingModule { 
    static components = [
        DisciplineListComponent
    ];
    static resolvers = [
        DisciplineListResolver
    ];
}
