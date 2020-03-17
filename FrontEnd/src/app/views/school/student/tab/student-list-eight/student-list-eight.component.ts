import { ConfigConstant } from './../../../../../shared/constants/config.constant';
import { ConfigMesageConstant } from './../../../../../shared/constants/configmessage.constant';
import { Student } from './../../../../../shared/models/student.model';
import { NotifyService } from './../../../../../shared/services/notify.service';
import { StudentService } from './../../../../../shared/services/student.service';
import { PagingParams } from './../../../../../shared/params/paging.param';
import { Pagination, PaginatedResult } from './../../../../../shared/models/pagination.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService, NzModalService } from 'ng-zorro-antd';
import { StudentCreateEditModalComponent } from '../../modal/student-create-edit-modal/student-create-edit-modal.component';
import { StudentChangeGradeModalComponent } from '../../modal/student-change-grade-modal/student-change-grade-modal.component';

@Component({
  selector: 'app-student-list-eight',
  templateUrl: './student-list-eight.component.html',
  styleUrls: ['./student-list-eight.component.css']
})
export class StudentListEightComponent implements OnInit {
  pageNumber = ConfigConstant.PAGE_INDEX;
  pageSize = ConfigConstant.PAGE_SIZE;
  isAllDisplayDataChecked = false;
  reportEnrollment: any;
  isIndeterminate = false;
  listChangeStudent = [];
  mapOfCheckedId: { [key: string]: boolean } = {};
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
    private modalService: NzModalService
  ) { }

  ngOnInit() {
    this.loading = true;
    this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '2').subscribe(
      (res: PaginatedResult<Student[]>) => {
        this.loading = false;
        this.pagination = res.pagination;
        this.dataSet = res.result;
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
    this.studentService.GetStudentByLevelPaging(pagination.currentPage, pagination.itemsPerPage, '2', this.pagingParams)
      .subscribe((res: PaginatedResult<Student[]>) => {
        this.loading = false;
        this.pagination = res.pagination;
        this.dataSet = res.result;
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

  addNew() {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Thêm mới học sinh',
      nzContent: StudentCreateEditModalComponent,
      nzWidth: 650,
      nzContentParams: {
        student: {},
        isAddNew: true
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data: Student) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Chỉnh sửa học sinh',
      nzContent: StudentCreateEditModalComponent,
      nzWidth: 650,
      nzContentParams: {
        student: data,
        levelEnum: data.gradeVM.levelEnum,
        isAddNew: false
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  delete(id: any) {
    this.notifyService.confirm('Bạn có chắc muốn xóa không?', () => {
      this.studentService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }

  changeGrade() {
    this.listChangeStudent = [];
    const propertys: any = Object.keys(this.mapOfCheckedId);
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < propertys.length; i++) {
      if (this.mapOfCheckedId[propertys[i]] === true) {
        this.listChangeStudent.push(propertys[i]);
      }
    }
    if (this.listChangeStudent.length > 0) {
      const modal = this.modalService.create({
        nzTitle: 'Chuyển lớp',
        nzContent: StudentChangeGradeModalComponent,
        nzWidth: 450,
        nzComponentParams: {
          levelEnum: 2,
          studentsId: this.listChangeStudent
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
    } else {
      this.notifyService.warning('Chưa chọn học sinh muốn chuyển lớp !');
    }
  }

  // setup for table
  checkAll(value: boolean) {
    this.listChangeStudent = [];
    this.dataSet.forEach(item => {
      this.mapOfCheckedId[item.id] = value;
    });
    this.refreshStatus();
  }

  refreshStatus() {
    this.isAllDisplayDataChecked = this.dataSet.every(item => this.mapOfCheckedId[item.id]);
    this.isIndeterminate =
      this.dataSet.some(item => this.mapOfCheckedId[item.id]) && !this.isAllDisplayDataChecked;
  }
}

