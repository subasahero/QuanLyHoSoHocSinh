import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { RewardService } from './../../../../../shared/services/reward.service';
import { StudentService } from './../../../../../shared/services/student.service';
import { DetailRewardService } from './../../../../../shared/services/detail-reward.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { DetailReward } from './../../../../../shared/models/detailreward.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-detail-reward-create-edit-modal',
  templateUrl: './detail-reward-create-edit-modal.component.html',
  styleUrls: ['./detail-reward-create-edit-modal.component.css']
})
export class DetailRewardCreateEditModalComponent implements OnInit {
  @Input() detailReward: DetailReward;
  @Input() isAddNew: boolean;
  studentList: any;
  rewardList: any;
  detailRewardForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private detailRewardService: DetailRewardService,
    private studentService: StudentService,
    private rewardService: RewardService,
    private drawerRef: NzDrawerRef
  ) { }

  ngOnInit() {
    const date = new Date();
    this.createForm();
    this.detailRewardForm.reset();
    this.getRewardList();
    this.getStudentList();
    if (this.isAddNew) {
      this.detailRewardForm.get('dateReward').setValue(this.formatDate(new Date()));
    }
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.detailRewardForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.detailRewardForm.controls) {
        this.detailRewardForm.controls[i].markAsDirty();
        this.detailRewardForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const data: DetailReward = this.detailRewardForm.getRawValue();
    if (this.isAddNew) {
      this.detailRewardService.addNew(data).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    } else {
      this.detailRewardService.update(data).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }
  }

  createForm() {
    this.detailRewardForm = this.fb.group({
      id: [null],
      studentId: [null, [Validators.required]],
      rewardId: [null, [Validators.required]],
      reason: [null, [Validators.required]],
      gift: [null, [Validators.required]],
      dateReward: [null, [Validators.required]],
      createdDate: [null],
      modifiedDate: [null],
      status: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }

  getStudentList() {
    this.studentService.getAll().subscribe(
      (res: any[]) => {
        this.studentList = res.filter(x => x.gradeVM.levelEnum <= 3);
      }
    );
  }

  getRewardList() {
    this.rewardService.getAll().subscribe(
      res => {
        this.rewardList = res;
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
