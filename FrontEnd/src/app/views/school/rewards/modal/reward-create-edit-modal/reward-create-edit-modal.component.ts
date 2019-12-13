import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { RewardService } from './../../../../../shared/services/reward.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Reward } from './../../../../../shared/models/reward.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-reward-create-edit-modal',
  templateUrl: './reward-create-edit-modal.component.html',
  styleUrls: ['./reward-create-edit-modal.component.css']
})
export class RewardCreateEditModalComponent implements OnInit {
  @Input() reward: Reward;
  @Input() isAddNew: boolean;
  rewardForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private rewardService: RewardService,
    private drawerRef: NzDrawerRef
  ) { }

  ngOnInit() {
    this.createForm();
    this.rewardForm.reset();
    this.rewardForm.patchValue(this.reward);
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.rewardForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.rewardForm.controls) {
        this.rewardForm.controls[i].markAsDirty();
        this.rewardForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const reward: Reward = this.rewardForm.getRawValue();
    this.rewardService.getCheckExistsRecord(reward.number,reward.description).subscribe(
      (res: boolean) => {
        if (res) {
          this.notify.error(ConfigMesageConstant.MESSAGE_SAME_RECORD);
          this.loadingSaveChanges = false;
        } else {
          if (this.isAddNew) {
            this.rewardService.addNew(reward).subscribe((res: any) => {
              if (res) {
                this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
                this.close();
              }
      
              this.loadingSaveChanges = false;
            }, _ => this.loadingSaveChanges = false);
          } else {
            this.rewardService.update(reward).subscribe((res: any) => {
              if (res) {
                this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
                this.close();
              }
      
              this.loadingSaveChanges = false;
            }, _ => this.loadingSaveChanges = false);
          }
        }
      }
    );
  }

  createForm() {
    this.rewardForm = this.fb.group({
      id: [null],
      number: [null, [Validators.required]],
      description: [null, [Validators.required]],
      createdDate: [null],
      modifiedDate: [null],
      status: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }
}
