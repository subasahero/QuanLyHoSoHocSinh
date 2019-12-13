import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {
  isCollapsed = false;
  constructor(
    private router: Router,
    private notification: NzNotificationService
    ) { }

  ngOnInit() {
  }

  profile() {
    this.notification.create(
      'info',
      'Thông báo',
      'Chức năng hiện đang phát triển mời bạn quay lại sau!'
      );
  }

  logout() {
    localStorage.removeItem('logined');
    this.router.navigate(['/login']);
  }
}
