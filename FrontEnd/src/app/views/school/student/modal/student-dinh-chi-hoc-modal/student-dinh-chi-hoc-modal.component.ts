import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd';
import { GradesService } from 'src/app/shared/services/grades.service';
import { StudentService } from 'src/app/shared/services/student.service';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { ConfigMesageConstant } from 'src/app/shared/constants/configmessage.constant';

@Component({
  selector: 'app-student-dinh-chi-hoc-modal',
  templateUrl: './student-dinh-chi-hoc-modal.component.html',
  styleUrls: ['./student-dinh-chi-hoc-modal.component.css']
})
export class StudentDinhChiHocModalComponent implements OnInit {
  @Input() studentId: any;
  gradeList = [];
  dinhChiHocForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private modal: NzModalRef,
    private fb: FormBuilder,
    private gradeService: GradesService,
    private studentService: StudentService,
    private notifyService: NotifyService
  ) { }

  ngOnInit() {
    this.createForm();
    this.getGradeList();
    this.dinhChiHocForm.patchValue({
      id: this.studentId,
    });
  }

  destroyModal() {
    this.modal.destroy(false);
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.dinhChiHocForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.dinhChiHocForm.controls) {
        this.dinhChiHocForm.controls[i].markAsDirty();
        this.dinhChiHocForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }
    const data = this.dinhChiHocForm.getRawValue();

    this.studentService.DinhChiHoc(data).subscribe(
      (res: any) => {
        this.loadingSaveChanges = false;
        this.modal.destroy(true);
      }, _ => this.notifyService.error(ConfigMesageConstant.MESSAGE_ERRO)
    );
  }


  createForm() {
    this.dinhChiHocForm = this.fb.group({
      id: [null],
      gradeId: [null, Validators.required],
    });
  }

  getGradeList() {
    this.gradeService.getAll().subscribe(
      (res: any[]) => {
        this.gradeList = res.filter(x => x.levelEnum === 5);
        console.log(this.gradeList);
      }
    );
  }
}
