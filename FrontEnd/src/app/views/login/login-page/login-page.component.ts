import { NotifyService } from './../../../shared/services/notify.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  url: string;
  loginForm: FormGroup;
  data: any;
  isLoadingLogin = false;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private notify: NotifyService,
  ) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.loginForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [null]
    });
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.loginForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.loginForm.controls) {
        this.loginForm.controls[i].markAsDirty();
        this.loginForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }
    const login = this.loginForm.getRawValue();
    if (login.userName === 'daohieu' && login.password === '123456') {
      localStorage.setItem('logined', 'Chào mừng đào hiếu!');
      this.router.navigate(['']);
    } else {
      this.notify.error('Sai tài khoản hoặc mật khẩu mời nhập lại!');
      this.loadingSaveChanges = true;
    }
  }
}
