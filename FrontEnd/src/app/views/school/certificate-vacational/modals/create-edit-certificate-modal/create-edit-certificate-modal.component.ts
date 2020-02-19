import { Student } from './../../../../../shared/models/student.model';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { Certificate } from './../../../../../shared/models/certificate.model';
import { CertificateService } from './../../../../../shared/services/certificate.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-create-edit-certificate-modal',
  templateUrl: './create-edit-certificate-modal.component.html',
  styleUrls: ['./create-edit-certificate-modal.component.css']
})
export class CreateEditCertificateModalComponent implements OnInit {
  @Input() student: Student;
  @Input() certificate: Certificate;
  @Input() isAddNew: boolean;
  checkExistsRecord = true;
  certificateForm: FormGroup;
  loadingSaveChanges: boolean;
  listCareer = [
    { label: 'Nấu Ăn', value: '0' },
    { label: 'Điện dân dụng', value: '1' },
    { label: 'Thợ xây', value: '2' },
  ];
  listCertificateType = [
    { label: 'Kém', value: '0' },
    { label: 'Khá', value: '1' },
    { label: 'Giỏi', value: '2' },
  ];
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private certificateService: CertificateService,
    private drawerRef: NzDrawerRef
  ) { }

  ngOnInit() {
    this.createForm();
    this.certificateForm.reset();
    this.certificateForm.patchValue(this.certificate);
    if (this.isAddNew) {
      this.certificateForm.patchValue({
        ...this.certificate,
        studentId: this.student.id,
        subjectCareer: 'Nấu Ăn',
        certificateType: '2',
      })
    } else {
      this.certificateForm.patchValue({
        ...this.certificate,
        certificateType: this.certificate.certificateType.toString(),
      })
    }
  }

  saveChanges() {
    this.loadingSaveChanges = false;
    if (this.certificateForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.certificateForm.controls) {
        this.certificateForm.controls[i].markAsDirty();
        this.certificateForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const certificate: Certificate = this.certificateForm.getRawValue();
    if (this.isAddNew) {
      // tslint:disable-next-line: no-shadowed-variable
      this.certificateService.addNew(certificate).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    } else {
      // tslint:disable-next-line: no-shadowed-variable
      this.certificateService.update(certificate).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }
  }

  createForm() {
    this.certificateForm = this.fb.group({
      id: [null],
      studentId: [null],
      subjectCareer: [null, Validators.required],
      certificateType: [null, Validators.required],
    });
  }

  close(): void {
    this.drawerRef.close();
  }
}
