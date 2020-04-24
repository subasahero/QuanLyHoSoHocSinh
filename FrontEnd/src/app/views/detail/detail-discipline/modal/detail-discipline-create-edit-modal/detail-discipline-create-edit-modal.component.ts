import { DisciplineService } from './../../../../../shared/services/discipline.service';
import { StudentService } from './../../../../../shared/services/student.service';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { DetailDisciplineService } from './../../../../../shared/services/detail-discipline.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { DetailDiscipline } from './../../../../../shared/models/detaildiscipline.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-detail-discipline-create-edit-modal',
  templateUrl: './detail-discipline-create-edit-modal.component.html',
  styleUrls: ['./detail-discipline-create-edit-modal.component.css']
})
export class DetailDisciplineCreateEditModalComponent implements OnInit {
  @Input() detailDiscipline: DetailDiscipline;
  @Input() isAddNew: boolean;
  studentList: any;
  disciplineList: any;
  detailDisciplineForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private detailDisciplineService: DetailDisciplineService,
    private studentService: StudentService,
    private disciplineService: DisciplineService,
    private drawerRef: NzDrawerRef
  ) { }

  ngOnInit() {
    this.createForm();
    this.detailDisciplineForm.reset();
    this.detailDisciplineForm.patchValue(this.detailDiscipline);
    this.getDisciplineList();
    this.getStudentList();
    if(this.isAddNew) {
      this.detailDisciplineForm.get('datePunish').setValue(this.formatDate(new Date()));
    }
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.detailDisciplineForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.detailDisciplineForm.controls) {
        this.detailDisciplineForm.controls[i].markAsDirty();
        this.detailDisciplineForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const data: DetailDiscipline = this.detailDisciplineForm.getRawValue();
    if (this.isAddNew) {
      this.detailDisciplineService.addNew(data).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    } else {
      this.detailDisciplineService.update(data).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }
  }

  createForm() {
    this.detailDisciplineForm = this.fb.group({
      id: [null],
      studentId: [null, [Validators.required]],
      disciplineId: [null, [Validators.required]],
      reason: [null, [Validators.required]],
      punishment: [null, [Validators.required]],
      datePunish: [null, [Validators.required]],
      createdDate: [null],
      modifiedDate: [null],
      status: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  getStudentList(){
    this.studentService.getAll().subscribe(
      (res: any[]) => {
        this.studentList = res.filter(x => x.gradeVM.levelEnum <= 3);
      }
    );
  }

  getDisciplineList(){
    this.disciplineService.getAll().subscribe(
      res => {
        this.disciplineList = res;
      }
    );
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
}
