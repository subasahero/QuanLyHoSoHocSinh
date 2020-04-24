import { Component, OnInit, Input } from '@angular/core';
import { Student } from 'src/app/shared/models/student.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { DiemlopsauService } from 'src/app/shared/services/diemlopsau.service';
import { StudentService } from 'src/app/shared/services/student.service';
import { NzDrawerRef } from 'ng-zorro-antd';
import { ConfigMesageConstant } from 'src/app/shared/constants/configmessage.constant';
import { DiemlopbayService } from 'src/app/shared/services/diemlopbay.service';
import { DiemloptamService } from 'src/app/shared/services/diemloptam.service';
import { DiemlopchinService } from 'src/app/shared/services/diemlopchin.service';

@Component({
  selector: 'app-add-edit-diem-lop-sau',
  templateUrl: './add-edit-diem-lop-sau.component.html',
  styleUrls: ['./add-edit-diem-lop-sau.component.css']
})
export class AddEditDiemLopSauComponent implements OnInit {
  @Input() student: any;
  @Input() loaiHocKy: any;
  @Input() bangDiemData = {};
  @Input() khoiData: any;
  @Input() isAddNew: boolean;
  bangDiemForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private studentService: StudentService,
    private diemLopSauService: DiemlopsauService,
    private diemLopBayService: DiemlopbayService,
    private diemLopTamService: DiemloptamService,
    private diemLopChinService: DiemlopchinService,
    private drawerRef: NzDrawerRef
  ) { }

  ngOnInit() {
    this.createForm();
    if(this.isAddNew) {
      this.bangDiemForm.patchValue({
        studentId: this.student.id,
        loai: this.loaiHocKy
      });
    } else {
      this.bangDiemForm.patchValue({
        ...this.bangDiemData
      });
    }
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.bangDiemForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.bangDiemForm.controls) {
        this.bangDiemForm.controls[i].markAsDirty();
        this.bangDiemForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const studentScore = this.bangDiemForm.getRawValue();
    console.log(studentScore);
    if (this.isAddNew) {
      // tslint:disable-next-line: no-shadowed-variable
      if(this.khoiData == 6) {
        this.diemLopSauService.addNew(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      } else if(this.khoiData == 7) {
        this.diemLopBayService.addNew(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      }else if(this.khoiData == 8) {
        this.diemLopTamService.addNew(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      }else if(this.khoiData == 9) {
        this.diemLopChinService.addNew(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      }
    } else {
      // tslint:disable-next-line: no-shadowed-variable
      if(this.khoiData == 6) {
        this.diemLopSauService.update(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      } else if(this.khoiData == 7) { 
        this.diemLopBayService.update(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      }else if(this.khoiData == 8) { 
        this.diemLopTamService.update(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      }else if(this.khoiData == 9) { 
        this.diemLopChinService.update(studentScore).subscribe((res: any) => {
          if (res) {
            this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
            this.close();
          }
  
          this.loadingSaveChanges = false;
        }, _ => this.loadingSaveChanges = false);
      }
    }
  }

  createForm() {
    this.bangDiemForm = this.fb.group({
      id: [null],
      studentId: [null, Validators.required],
      toan: [0],
      nguVan: [0],
      ngoaiNgu: [0],
      tin: [0],
      lichSu: [0],
      diaLy: [0],
      giaoDucCongDan: [0],
      congNghe: [0],
      vatLy: [0],
      sinhHoc: [0],
      amNhac: [0],
      myThuat: [0],
      diemTrungBinhCong: [0],
      loai: [0],
    });
  }

  close(): void {
    this.drawerRef.close();
  }
}
