import { DefaultLayoutComponent } from './containers/default-layout/default-layout.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './views/login/login-page/login-page.component';
import { AuthGuardService } from './shared/services/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'truong-hoc',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Trang chủ'
    },
    canActivate: [AuthGuardService],
    children: [
      {
        path: 'bao-cao',
        loadChildren: () => import('./views/report/report.module').then(m => m.ReportModule)
      },
      {
        path: 'truong-hoc',
        loadChildren: () => import('./views/school/shool.module').then(m => m.SchoolModule)
      },
      {
        path: 'chi-tiet',
        loadChildren: () => import('./views/detail/detail.module').then(m => m.DetailModule)
      },
    ]
  },
  {
    path: 'login',
    component: LoginPageComponent,
    data: {
      title: 'Đăng nhập'
    },
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  static components = [
    DefaultLayoutComponent,
    LoginPageComponent
  ];
}
