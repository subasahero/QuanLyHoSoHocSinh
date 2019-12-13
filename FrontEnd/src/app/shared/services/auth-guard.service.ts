import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router) { }

  canActivate(): boolean {
    if (localStorage.getItem('logined') !== 'Chào mừng đào hiếu!') {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
