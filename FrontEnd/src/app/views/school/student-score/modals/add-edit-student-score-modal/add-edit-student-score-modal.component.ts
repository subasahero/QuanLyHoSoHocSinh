import { Grade } from './../../../../../shared/models/grade.model';
import { StudentScoreService } from './../../../../../shared/services/student-score.service.ts.service';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { StudentService } from './../../../../../shared/services/student.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Student } from './../../../../../shared/models/student.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-add-edit-student-score-modal',
  templateUrl: './add-edit-student-score-modal.component.html',
  styleUrls: ['./add-edit-student-score-modal.component.css']
})
export class AddEditStudentScoreModalComponent implements OnInit {
  @Input() student: Student;
  @Input() isAddNew: boolean;
  studentScoreForm: FormGroup;
  loadingSaveChanges: boolean;
  // disable
  levelSix = true;
  levelSeven = true;
  levelEight = true;
  levelNine = true;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private studentService: StudentService,
    private studentScoreService: StudentScoreService,
    private drawerRef: NzDrawerRef
  ) { }

  ngOnInit() {
    this.enableFormByLeveEnum();
    this.studentScoreForm.reset();
    this.setDefauleValue();
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.studentScoreForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.studentScoreForm.controls) {
        this.studentScoreForm.controls[i].markAsDirty();
        this.studentScoreForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const studentScore = this.studentScoreForm.getRawValue();
    if (this.isAddNew) {
      // tslint:disable-next-line: no-shadowed-variable
      this.studentScoreService.addNew(studentScore).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    } else {
      // tslint:disable-next-line: no-shadowed-variable
      this.studentScoreService.update(studentScore).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }
  }

  createForm() {
    this.studentScoreForm = this.fb.group({
      id: [null],
      studentId: [null, Validators.required],
      semesterOneLevelSix: [{ value: 0, disabled: this.levelSix }, Validators.required],
      semesterTwoLevelSix: [{ value: 0, disabled: this.levelSix }, Validators.required],
      semesterOneLevelSeven: [{ value: 0, disabled: this.levelSeven }],
      semesterTwoLevelSeven: [{ value: 0, disabled: this.levelSeven }],
      semesterOneLevelEight: [{ value: 0, disabled: this.levelEight }],
      semesterTwoLevelEight: [{ value: 0, disabled: this.levelEight }],
      semesterOneLevelNine: [{ value: 0, disabled: this.levelNine }],
      semesterTwoLevelNine: [{ value: 0, disabled: this.levelNine }],
      createdDate: [null],
      modifiedDate: [null],
      status: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  enableFormByLeveEnum() {
    const grade: Grade = this.student.gradeVM;
    if (+grade.levelEnum === 0) {
      this.levelSix = false;
    } else if (+grade.levelEnum === 1) {
      this.levelSeven = false;
    } else if (+grade.levelEnum === 2) {
      this.levelEight = false;
    } else if (+grade.levelEnum === 3) {
      this.levelNine = false;
    }
    this.createForm();
  }

  setDefauleValue() {
    if (this.isAddNew) {
      this.studentScoreForm.get('studentId').setValue(this.student.id);
      this.studentScoreForm.get('semesterOneLevelSix').setValue(0);
      this.studentScoreForm.get('semesterTwoLevelSix').setValue(0);
      this.studentScoreForm.get('semesterOneLevelSeven').setValue(0);
      this.studentScoreForm.get('semesterTwoLevelSeven').setValue(0);
      this.studentScoreForm.get('semesterOneLevelEight').setValue(0);
      this.studentScoreForm.get('semesterTwoLevelEight').setValue(0);
      this.studentScoreForm.get('semesterOneLevelNine').setValue(0);
      this.studentScoreForm.get('semesterTwoLevelNine').setValue(0);
    } else {
      this.studentScoreForm.patchValue(this.student.studentScoreVM);
    }
  }
}
