import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        data: {
            breadcrumb: 'Trường học'
        },
        children: [
            {
                path: 'chung-chi-nghe',
                // tslint:disable-next-line: max-line-length
                loadChildren: () => import('./certificate-vacational/certificate-vacational.module').then(m => m.CertificateVacationalModule)
            },
            {
                path: 'diem-hoc-sinh',
                loadChildren: () => import('./student-score/student-score.module').then(m => m.StudentScoreModule)
            },
            {
                path: 'hoc-sinh',
                loadChildren: () => import('./student/student.module').then(m => m.StudentModule)
            },
            {
                path: 'ky-luat',
                loadChildren: () => import('./disciplines/disciplines.module').then(m => m.DisciplinesModule)
            },
            {
                path: 'lop',
                loadChildren: () => import('./grades/grades.module').then(m => m.GradesModule)
            },
            {
                path: 'khen-thuong',
                loadChildren: () => import('./rewards/rewards.module').then(m => m.RewardsModule)
            },
            {
                path: '',
                redirectTo: 'hoc-sinh',
                pathMatch: 'full'
            }
        ]
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SchoolRoutingModule { }
