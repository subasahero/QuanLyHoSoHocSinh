import { StudentScoreService } from './../../../../../shared/services/student-score.service.ts.service';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { Student } from './../../../../../shared/models/student.model';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { StudentService } from './../../../../../shared/services/student.service';
import { PagingParams } from './../../../../../shared/params/paging.param';
import { Pagination, PaginatedResult } from './../../../../../shared/models/pagination.model';
import { ConfigConstant } from './../../../../../shared/constants/config.constant';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService, NzModalService } from 'ng-zorro-antd';
import { AddEditStudentScoreModalComponent } from '../../modals/add-edit-student-score-modal/add-edit-student-score-modal.component';
import { NextLevelStudentComponent } from '../../modals/add-edit-student-score-modal/next-level-student/next-level-student.component';

@Component({
  selector: 'app-tab-level-nine',
  templateUrl: './tab-level-nine.component.html',
  styleUrls: ['./tab-level-nine.component.css']
})
export class TabLevelNineComponent implements OnInit {
  pageNumber = ConfigConstant.PAGE_INDEX;
  pageSize = ConfigConstant.PAGE_SIZE;
  dataSet = [];
  loading = true;
  sortValue = null;
  sortKey = null;
  pagination = {} as Pagination;
  pagingParams: PagingParams = {
    keyword: '',
    sortKey: '',
    searchKey: '',
    searchValue: '',
    levelIdValue: '',
    gradeId: ''
  };

  constructor(
    private studentService: StudentService,
    private route: ActivatedRoute,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService,
    private modalService: NzModalService,
    private studentScoreService: StudentScoreService
  ) { }

  ngOnInit() {
    this.loading = true;
    this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '3').subscribe(
      (res: PaginatedResult<Student[]>) => {
        this.loading = false;
        this.pagination = res.pagination;
        this.dataSet = res.result;
        console.log(this.dataSet);
      }
    );
  }

  sort(sort: { key: string, value: string }): void {
    this.pagingParams.sortKey = sort.key;
    this.pagingParams.sortValue = sort.value;
    this.loadData();
  }

  loadData(reset: boolean = false) {
    const pagination: Pagination = this.pagination;
    if (reset) {
      pagination.currentPage = 1;
    }
    this.loading = true;
    this.studentService.GetStudentByLevelPaging(pagination.currentPage, pagination.itemsPerPage, '3', this.pagingParams)
      .subscribe((res: PaginatedResult<Student[]>) => {
        this.loading = false;
        this.pagination = res.pagination;
        this.dataSet = res.result;
      });
  }

  addNew(data: Student) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Thêm mới bảng điểm',
      nzContent: AddEditStudentScoreModalComponent,
      nzWidth: 650,
      nzContentParams: {
        student: data,
        isAddNew: true
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data: Student) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Cập nhật bảng điểm',
      nzContent: AddEditStudentScoreModalComponent,
      nzWidth: 650,
      nzContentParams: {
        student: data,
        isAddNew: false
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  search(keyword: string) {
    this.pagingParams.keyword = keyword;
    this.loadData();
  }

  searchColumn(searchKey: string) {
    this.pagingParams.searchKey = searchKey;
    this.loadData(true);
  }

  reset() {
    this.pagingParams.searchKey = '';
    this.pagingParams.searchValue = '';
    this.loadData(true);
  }


  delete(id: any) {
    this.notifyService.confirm('Bạn có chắc muốn xóa không?', () => {
      this.studentScoreService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }

  nextLevel(data: Student) {
    const listChangeStudent = [data.id];
    const modal = this.modalService.create({
      nzTitle: 'Lên lớp',
      nzContent: NextLevelStudentComponent,
      nzWidth: 450,
      nzComponentParams: {
        levelEnum: data.gradeVM.levelEnum,
        studentsId: listChangeStudent
      }
    });

    modal.afterClose.subscribe(
      (result: boolean) => {
        if (result) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_CHANGE_GRADE_SUCCESS_MODAL);
          this.loadData();
        }
      }
    );
  }
}
