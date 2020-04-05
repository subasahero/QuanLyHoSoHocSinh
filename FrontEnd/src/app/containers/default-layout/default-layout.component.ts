import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NzNotificationService, NzDrawerService } from 'ng-zorro-antd';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { ChangeUserPasswordComponent } from '../modals/change-user-password/change-user-password.component';
@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {
  isCollapsed = false;
  roleUser = this.jwtHelper.decodeToken(localStorage.getItem('token'));
  constructor(
    private router: Router,
    private notification: NzNotificationService,
    public jwtHelper: JwtHelperService,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService
    ) { }

  ngOnInit() {
    console.log(this.roleUser);
  }

  profile() {
    this.notification.create(
      'info',
      'Thông báo',
      'Chức năng hiện đang phát triển mời bạn quay lại sau!'
      );
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  changePassword() {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Đổi mật khẩu',
      nzContent: ChangeUserPasswordComponent,
      nzWidth: 400,
      nzContentParams: {
        userData: this.roleUser,
      }
    });
    drawerRef.afterClose.subscribe(() => {
      //this.loadData();
    });
  }
}
