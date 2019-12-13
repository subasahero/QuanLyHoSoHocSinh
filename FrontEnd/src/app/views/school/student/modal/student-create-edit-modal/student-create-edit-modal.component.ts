import { GradesService } from 'src/app/shared/services/grades.service';
import { Grade } from './../../../../../shared/models/grade.model';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { StudentService } from './../../../../../shared/services/student.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Student } from './../../../../../shared/models/student.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-student-create-edit-modal',
  templateUrl: './student-create-edit-modal.component.html',
  styleUrls: ['./student-create-edit-modal.component.css']
})
export class StudentCreateEditModalComponent implements OnInit {
  @Input() student: Student;
  @Input() isAddNew: boolean;
  sexList = [{ value: 'Nam' }, { value: 'Nữ' }, { value: 'Khác' }];
  gradeList = [];
  studentForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private studentService: StudentService,
    private drawerRef: NzDrawerRef,
    private gradeService: GradesService
  ) { }

  ngOnInit() {
    this.createForm();
    this.studentForm.reset();
    this.studentForm.patchValue(this.student);
    this.getListGrade();
  }

  saveChanges() {
    this.loadingSaveChanges = true;
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
    if (this.isAddNew) {
      // tslint:disable-next-line: no-shadowed-variable
      this.studentService.addNew(student).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    } else {
      // tslint:disable-next-line: no-shadowed-variable
      this.studentService.update(student).subscribe((res: any) => {
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
      dateGoShcool: [null, Validators.required],
      createdDate: [null],
      modifiedDate: [null],
      status: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  getListGrade() {
    this.gradeService.getAll().subscribe(
      (res: Grade[]) => {
        res.forEach((x: Grade)=> {
          if(parseInt(x.levelEnum) < 4) {
            this.gradeList.push(x);
          }
        });
      }
    );
  }

  levelIdChange(event) {
    console.log(event);
  }
}
