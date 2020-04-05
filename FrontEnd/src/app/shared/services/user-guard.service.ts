import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NotifyService } from './notify.service';
@Injectable({
  providedIn: 'root'
})
export class UserGuardService implements CanActivate {
  constructor(
      private router: Router,
      public jwtHelper: JwtHelperService,
      private notify: NotifyService,
      ) { }

  canActivate(): boolean {
    if (this.jwtHelper.decodeToken(localStorage.getItem('token')).role === 'Admin') {
      return true;
    }
    this.notify.error('Bạn không có quyền truy cập!');
    return false;
  }
}
