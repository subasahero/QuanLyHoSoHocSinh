import { Grade } from './../../../../../../shared/models/grade.model';
import { ConfigMesageConstant } from './../../../../../../shared/constants/configmessage.constant';
import { ChangeStudentGradeModel } from './../../../../../../shared/models/change-student-grade.model';
import { NotifyService } from './../../../../../../shared/services/notify.service';
import { StudentService } from './../../../../../../shared/services/student.service';
import { GradesService } from './../../../../../../shared/services/grades.service';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-next-level-student',
  templateUrl: './next-level-student.component.html',
  styleUrls: ['./next-level-student.component.css']
})
export class NextLevelStudentComponent implements OnInit {
  @Input() levelEnum: any;
  @Input() studentsId: any[];
  gradeId: string;
  gradeList = [];
  changeGradeForm: FormGroup;
  loadingSaveChanges = false;
  constructor(
    private modal: NzModalRef,
    private fb: FormBuilder,
    private gradeService: GradesService,
    private studentService: StudentService,
    private notifyService: NotifyService
  ) { }

  ngOnInit() {
    this.createForm();
    this.changeGradeForm.reset();
    this.getGradeList();
  }

  createForm() {
    this.changeGradeForm = this.fb.group({
      studentsId: [null],
      gradeId: [null, Validators.required]
    });
  }

  destroyModal() {
    this.modal.destroy(false);
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.changeGradeForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.changeGradeForm.controls) {
        this.changeGradeForm.controls[i].markAsDirty();
        this.changeGradeForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }
    this.changeGradeForm.get(`studentsId`).setValue(this.studentsId);
    const changeGradeData: ChangeStudentGradeModel = this.changeGradeForm.getRawValue();

    this.studentService.changeGrade(changeGradeData).subscribe(
      (res: any) => {
        this.loadingSaveChanges = false;
        this.modal.destroy(true);
      }, _ => this.notifyService.error(ConfigMesageConstant.MESSAGE_ERRO)
    );
  }

  getGradeList() {
    this.gradeService.getAll().subscribe(
      (res: Grade[]) => {
        res.forEach((x: Grade) => {
          if ((+x.levelEnum) === (this.levelEnum + 1)) {
            this.gradeList.push(x);
          }
        });
      }
    );
  }
}
