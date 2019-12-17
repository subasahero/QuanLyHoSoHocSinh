import { CertificateVacationalListResolver } from './../../../shared/resolves/certificate-vacational-list.resolver';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CertificateVacationalListComponent } from './certificate-vacational-list/certificate-vacational-list.component';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Chứng chỉ nghề'
        },
        children: [
            {
                path: 'danh-sach',
                component: CertificateVacationalListComponent,
                data: {
                    breadcrumb: 'Danh sách'
                },
                resolve: { 'certificate-vacational-list': CertificateVacationalListResolver }
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
export class CertificateVacationalRoutingModule {
    static components = [
        CertificateVacationalListComponent
    ];
    static resolvers = [
        CertificateVacationalListResolver
    ];
}
