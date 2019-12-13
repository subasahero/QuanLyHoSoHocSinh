import { ConfigLevel } from './../../../../../shared/constants/config.level';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { LevelsServiceService } from './../../../../../shared/services/levels-service.service';
import { GradesService } from './../../../../../shared/services/grades.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Grade } from './../../../../../shared/models/grade.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-grade-create-edit-modal',
  templateUrl: './grade-create-edit-modal.component.html',
  styleUrls: ['./grade-create-edit-modal.component.css']
})
export class GradeCreateEditModalComponent implements OnInit {
  @Input() grade: Grade;
  @Input() isAddNew: boolean;
  checkExistsRecord = true;
  levelList = ConfigLevel.levelList;
  gradeForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private gradeService: GradesService,
    private drawerRef: NzDrawerRef,
    private levelService: LevelsServiceService
  ) { }

  ngOnInit() {
    this.createForm();
    this.gradeForm.reset();
    this.gradeForm.patchValue(this.grade);
    if(!this.isAddNew) {
      this.gradeForm.get('levelEnum').setValue(this.grade.levelEnum + "");
    }
  }

  saveChanges() {
    this.loadingSaveChanges = false;
    if (this.gradeForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.gradeForm.controls) {
        this.gradeForm.controls[i].markAsDirty();
        this.gradeForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const grade: Grade = this.gradeForm.getRawValue();
    console.log(grade);
    this.gradeService.getCheckExistsRecord(grade.name, grade.levelEnum).subscribe(
        (res: boolean) => {
        if (res === false) {
          if (this.isAddNew) {
            // tslint:disable-next-line: no-shadowed-variable
            this.gradeService.addNew(grade).subscribe((res: any) => {
              if (res) {
                this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
                this.close();
              }

              this.loadingSaveChanges = false;
            }, _ => this.loadingSaveChanges = false);
          } else {
            // tslint:disable-next-line: no-shadowed-variable
            this.gradeService.update(grade).subscribe(( res: any) => {
              if (res) {
                this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
                this.close();
              }

              this.loadingSaveChanges = false;
            }, _ => this.loadingSaveChanges = false);
          }
        } else {
          this.notify.error(ConfigMesageConstant.MESSAGE_SAME_RECORD);
          this.loadingSaveChanges = false;
        }
      }
    );
  }

  createForm() {
    this.gradeForm = this.fb.group({
      id: [null],
      levelEnum: [null, Validators.required],
      name: [null, Validators.required],
      createdDate: [null],
      modifiedDate: [null],
      status: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  levelIdChange(event) {
    console.log(event);
  }
}
