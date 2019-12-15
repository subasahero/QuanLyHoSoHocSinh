import { ReportEnrollment } from './../../../../shared/models/report-enrollment';
import { ConfigMesageConstant } from './../../../../shared/constants/configmessage.constant';
import { Student } from './../../../../shared/models/student.model';
import { NotifyService } from './../../../../shared/services/notify.service';
import { StudentService } from './../../../../shared/services/student.service';
import { PagingParams } from './../../../../shared/params/paging.param';
import { Pagination, PaginatedResult } from './../../../../shared/models/pagination.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService, NzModalService } from 'ng-zorro-antd';
import { StudentCreateEditModalComponent } from '../modal/student-create-edit-modal/student-create-edit-modal.component';
import { StudentChangeGradeModalComponent } from '../modal/student-change-grade-modal/student-change-grade-modal.component';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {
  constructor(
  ) { }

  ngOnInit() {
  }
}
