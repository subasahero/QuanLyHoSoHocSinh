import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { DisciplineService } from './../../../../../shared/services/discipline.service';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { Discipline } from './../../../../../shared/models/discipline.model';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzDrawerRef } from 'ng-zorro-antd';

@Component({
  selector: 'app-discipline-create-edit-modal',
  templateUrl: './discipline-create-edit-modal.component.html',
  styleUrls: ['./discipline-create-edit-modal.component.css']
})
export class DisciplineCreateEditModalComponent implements OnInit {
  @Input() discipline: Discipline;
  @Input() isAddNew: boolean;
  disciplineForm: FormGroup;
  loadingSaveChanges: boolean;
  constructor(
    private fb: FormBuilder,
    private notify: NotifyService,
    private disciplineService: DisciplineService,
    private drawerRef: NzDrawerRef
  ) { }

  ngOnInit() {
    this.createForm();
    this.disciplineForm.reset();
    this.disciplineForm.patchValue(this.discipline);
  }

  saveChanges() {
    this.loadingSaveChanges = true;
    if (this.disciplineForm.invalid) {
      // tslint:disable-next-line: forin
      for (const i in this.disciplineForm.controls) {
        this.disciplineForm.controls[i].markAsDirty();
        this.disciplineForm.controls[i].updateValueAndValidity();
      }

      this.loadingSaveChanges = false;
      return;
    }

    const data: Discipline = this.disciplineForm.getRawValue();
    this.disciplineService.getCheckExistsRecord(data.number,data.description).subscribe(
      (res: boolean) => {
        if (res) {
          this.notify.error(ConfigMesageConstant.MESSAGE_SAME_RECORD);
          this.loadingSaveChanges = false;
        } else {
          if (this.isAddNew) {
            this.disciplineService.addNew(data).subscribe((res: any) => {
              if (res) {
                this.notify.success(ConfigMesageConstant.MESSAGE_CREATE_SUCCESS_MODAL);
                this.close();
              }
      
              this.loadingSaveChanges = false;
            }, _ => this.loadingSaveChanges = false);
          } else {
            this.disciplineService.update(data).subscribe((res: any) => {
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
    this.disciplineForm = this.fb.group({
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
