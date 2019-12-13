import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { LevelsServiceService } from './../../../../../shared/services/levels-service.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Level } from './../../../../../shared/models/level.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerService, NzDrawerRef } from 'ng-zorro-antd';
import { HttpClient } from '@angular/common/http';
import { ValidatorsExistsName } from 'src/app/shared/ValidatorCustom/CheckExistsName';
@Component({
  selector: 'app-level-create-edit-modal',
  templateUrl: './level-create-edit-modal.component.html',
  styleUrls: ['./level-create-edit-modal.component.css']
})
export class LevelCreateEditModalComponent implements OnInit {
  @Input() level: Level;
  @Input() isAddNew: boolean;
  levelForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private levelService: LevelsServiceService,
    private drawerRef: NzDrawerRef,
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.createForm();
    this.levelForm.reset();
    this.levelForm.patchValue(this.level);
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.levelForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.levelForm.controls) {
        this.levelForm.controls[i].markAsDirty();
        this.levelForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const level = this.levelForm.getRawValue();
    if (this.isAddNew) {
      this.levelService.addNew(level).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    } else {
      this.levelService.update(level).subscribe((res: any) => {
        if (res) {
          this.notify.success(ConfigMesageConstant.MESSAGE_UPADTE_SUCCESS_MODAL);
          this.close();
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }
  }

  createForm() {
    this.levelForm = this.fb.group({
      id: [null],
      name: [null, [Validators.required], [ValidatorsExistsName(this.levelService)]],
      createdDate: [null],
      modifiedDate: [null],
      status: [null]
    });
  }

  close(): void {
    this.drawerRef.close();
  }
}
