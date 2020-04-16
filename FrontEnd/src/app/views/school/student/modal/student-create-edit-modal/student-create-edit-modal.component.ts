import { GradesService } from 'src/app/shared/services/grades.service';
import { Grade } from './../../../../../shared/models/grade.model';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { StudentService } from './../../../../../shared/services/student.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Student } from './../../../../../shared/models/student.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';
import { EnvService } from 'src/app/env.service';

@Component({
  selector: 'app-student-create-edit-modal',
  templateUrl: './student-create-edit-modal.component.html',
  styleUrls: ['./student-create-edit-modal.component.css']
})
export class StudentCreateEditModalComponent implements OnInit {
  @Input() levelEnum: any;
  @Input() student: any;
  @Input() isAddNew: boolean;
  sexList = [{ value: 'Nam' }, { value: 'Nữ' }, { value: 'Khác' }];
  gradeList = [];
  studentForm: FormGroup;
  loadingSaveChanges: boolean;
  formData: FormData;
  loadingFile: boolean;
  fileName: any;
  visibleTagName = false;
  validExtentions = ['.jpg', '.png'];
  contentTooltip = '';
  imageLinkStudent: any;
  
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private studentService: StudentService,
    private drawerRef: NzDrawerRef,
    private gradeService: GradesService,
    private env: EnvService
  ) { }

  ngOnInit() {
    this.formData = new FormData();
    this.createForm();
    this.studentForm.reset();
    this.getListGrade();
    if (this.isAddNew) {
      this.studentForm.get(`dateGoShcool`).setValue(this.formatDate(new Date()));
    } else {
      this.studentForm.patchValue({
        ...this.student
      });
      this.imageLinkStudent = this.env.apiImg + 'img/' + this.student.imageLink;
    }
  }

  saveChanges() {
    //this.loadingSaveChanges = true;
    if (this.studentForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.studentForm.controls) {
        this.studentForm.controls[i].markAsDirty();
        this.studentForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const student = this.studentForm.getRawValue();
    this.formData.append('gradeId', student.gradeId);
    this.formData.append('code', student.code);
    this.formData.append('name', student.name);
    this.formData.append('sex', student.sex);
    this.formData.append('birthday', student.birthday);
    this.formData.append('birthLocate', student.birthLocate);
    this.formData.append('talent', student.talent);
    this.formData.append('imageLink', student.imageLink);
    this.formData.append('address', student.address);
    this.formData.append('dateGoShcool', student.dateGoShcool);
    this.formData.append('createdDate', student.createdDate);
    this.formData.append('modifiedDate', student.modifiedDate);
    this.formData.append('status', '' + student.status);
    if (this.isAddNew) {
      // tslint:disable-next-line: no-shadowed-variable
      this.studentService.addNew(this.formData).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    } else {
      this.formData.append('id', student.id);
      // tslint:disable-next-line: no-shadowed-variable
      this.studentService.update(this.formData).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }
  }

  createForm() {
    this.studentForm = this.fb.group({
      id: [null],
      gradeId: [null, Validators.required],
      code: [null],
      name: [null, Validators.required],
      sex: [null, Validators.required],
      birthday: [null, Validators.required],
      birthLocate: [null, Validators.required],
      talent: [null],
      imageLink: [null, Validators.required],
      address: [null],
      dateGoShcool: [null, Validators.required],
      createdDate: [null],
      modifiedDate: [null],
      status: [true]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  getListGrade() {
    this.gradeService.getAll().subscribe(
      (res: Grade[]) => {
        res.forEach((x: Grade) => {
          if (this.isAddNew === false) {
            if ((+x.levelEnum) === this.levelEnum) {
              this.gradeList.push(x);
            }
          } else {
            if ((+x.levelEnum) === 0) {
              this.gradeList.push(x);
            }
          }
        });
      }
    );
  }

  levelIdChange(event) {
    console.log(event);
  }

  formatDate(date) {
    let dd = date.getDate();
    let mm = date.getMonth() + 1;
    const yyyy = date.getFullYear();
    if (dd < 10) { dd = '0' + dd; }
    if (mm < 10) { mm = '0' + mm; }
    // date = mm + '/' + dd + '/' + yyyy;
    date = yyyy + '-' + mm + '-' + dd;
    return date;
  }

  onFileInput(event: any) {
    console.log(event);
    this.loadingFile = true;
    const files = event.target.files;

    if (files && files[0]) {
      if (!this.checkExtension(event.target.files[0].name, this.validExtentions)) {
        this.notify.warning('File không hợp lệ.');
        this.loadingFile = false;
        return;
      }

      if (!this.checkFileSize(event.target.files[0].size)) {
        this.notify.error('Dung lượng file vượt quá 2MB.');
        return;
      }

      this.formData.delete('file');
      this.formData.append('file', files[0]);
      this.fileName = event.target.files[0].name;
      this.studentForm.controls['imageLink'].setValue(this.fileName);
      this.visibleTagName = true;
      this.contentTooltip = this.fileName;
    } else {
      event.target.value = null;
      this.formData.delete('file');
      this.fileName = '';
      this.studentForm.controls['imageLink'].setValue(null);
      this.visibleTagName = false;
      this.contentTooltip = '';
    }

    this.loadingFile = false;
  }

  sliceTagName(tag: string): string {
    if (tag) {
      const lastDot = tag.lastIndexOf('.');
      const fileName = tag.substring(0, lastDot);
      const tagLength = fileName.length;
      if (tagLength > 20) {
        const fileExtention = tag.substring(lastDot, tag.length);
        const result = fileName.slice(0, 14) + '...' + fileName.slice(tagLength - 3, tagLength) + fileExtention;
        return result;
      } else {
        return tag;
      }
    } else {
      return '';
    }
  }

  handleClose() {
    this.formData.delete('file');
    this.fileName = '';
    this.studentForm.controls['imageLink'].setValue(null);
    this.visibleTagName = false;
    this.contentTooltip = '';
  }

  checkExtension(fileName: string, extentions: any) {
    return (new RegExp('(' + extentions.join('|').replace(/\./g, '\\.') + ')$')).test(fileName);
  }

  checkFileSize(fileSize) {
    if ((fileSize / 1024 / 1024) < 2048) { return true; }
    return false;
  }
}
