import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { NzDrawerRef } from 'ng-zorro-antd';
import { UserService } from 'src/app/shared/services/user.service';
import { ConfigMesageConstant } from 'src/app/shared/constants/configmessage.constant';
import { RoleService } from 'src/app/shared/services/role.service';

@Component({
  selector: 'app-create-edit-user',
  templateUrl: './create-edit-user.component.html',
  styleUrls: ['./create-edit-user.component.css']
})
export class CreateEditUserComponent implements OnInit {
  @Input() userData: any;
  @Input() isAddNew: boolean;
  userFormAddNew: FormGroup;
  userFormUpdate: FormGroup;
  loadingSaveChanges: boolean;
  listRole: any[] = [];
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private drawerRef: NzDrawerRef,
    private userService: UserService,
    private roleService: RoleService
  ) { }

  ngOnInit() {
    this.getListRole();
    this.createForm();
    this.createForm2();
    if(!this.isAddNew) {
      this.userFormUpdate.patchValue({
        ...this.userData
      });
    }
  }

  selectRole(data) {

  }
  
  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.isAddNew) {
      if (this.userFormAddNew.invalid) {
        // tslint:disable-next-line: forin
        for (const i in this.userFormAddNew.controls) {
          this.userFormAddNew.controls[i].markAsDirty();
          this.userFormAddNew.controls[i].updateValueAndValidity();
        }
  
        this.loadingSaveChanges = false;
        return;
      }
      const data = this.userFormAddNew.getRawValue();
      this.userService.addNew(data).subscribe((res: any) => {
        if (res) {
          this.loadingSaveChanges = false;
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }
      }, _ => this.loadingSaveChanges = false);
    } else {
      if (this.userFormUpdate.invalid) {
        // tslint:disable-next-line: forin
        for (const i in this.userFormUpdate.controls) {
          this.userFormUpdate.controls[i].markAsDirty();
          this.userFormUpdate.controls[i].updateValueAndValidity();
        }
  
        this.loadingSaveChanges = false;
        return;
      }
      console.log('somthing');
      const data = this.userFormUpdate.getRawValue();
      this.userService.update(data).subscribe(( res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
          this.loadingSaveChanges = false;
          this.close();
        }

      }, _ => this.loadingSaveChanges = false);
    }
  }

  createForm() {
    this.userFormAddNew = this.fb.group({
      id: [null],
      userName: [null, Validators.required],
      password: [null, Validators.required],
      fullName: [null],
      status: [true],
      roleId: [null]
    });
  }

  createForm2() {
    this.userFormUpdate = this.fb.group({
      id: [null],
      userName: [{value: null, disabled: true}],
      fullName: [null],
      status: [true],
      roleId: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  getListRole() {
    this.roleService.GetAll().subscribe(
      (res: any) => {
        this.listRole = res;
        console.log(res);
      }
    );
  }

}
