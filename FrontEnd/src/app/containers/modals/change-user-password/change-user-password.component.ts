import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { NzDrawerRef } from 'ng-zorro-antd';
import { UserService } from 'src/app/shared/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-user-password',
  templateUrl: './change-user-password.component.html',
  styleUrls: ['./change-user-password.component.css']
})
export class ChangeUserPasswordComponent implements OnInit {
  @Input() userData: any;
  userForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private drawerRef: NzDrawerRef,
    private userService: UserService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.createForm();
    this.userForm.patchValue({
      userId: this.userData.nameid,
      userName: this.userData.unique_name,
    });
  }

  createForm() {
    this.userForm = this.fb.group({
      userId: [null],
      userName: [{value: null, disabled: true}],
      currentPassword: [null, Validators.required],
      newPassword: [null, Validators.required],
      checkPassword: [null, Validators.required]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.userForm.invalid) {
      for (const i in this.userForm.controls) {
        this.userForm.controls[i].markAsDirty();
        this.userForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }
    const data = this.userForm.getRawValue();
    if(data.newPassword !== data.checkPassword) {
      this.notify.error('Mật khẩu mới và nhập lại phải giống nhau!');
      return this.loadingSaveChanges = false;
    } else {
      this.userService.changePasswordForUser(data).subscribe(
        (res: any) => {
          this.close();
          this.notify.success('Đổi mật khẩu thành công!')
          localStorage.removeItem('token');
          this.router.navigate(['/login']);
        },
        err => {
          this.notify.error('Có lỗi xảy ra!')
          this.loadingSaveChanges = false;
        }
      );
    }
  }
}
