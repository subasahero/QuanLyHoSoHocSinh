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
import { AddEditStudentScoreModalComponent } from '../../modals/add-edit-student-score-modal/add-edit-student-score-modal.component';
@Component({
  selector: 'app-tab-level-six',
  templateUrl: './tab-level-six.component.html',
  styleUrls: ['./tab-level-six.component.css']
})
export class TabLevelSixComponent implements OnInit {
  pageNumber = ConfigConstant.PAGE_INDEX;
  pageSize = ConfigConstant.PAGE_SIZE;
  dataSet = [];
  loading = true;
  sortValue = null;
  sortKey = null;
  pagination = {};
  pagingParams: PagingParams = {
    keyword: '',
    sortKey: '',
    searchKey: '',
    searchValue: '',
    levelIdValue: '',
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
    this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '0').subscribe(
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
    this.studentService.GetStudentByLevelPaging(pagination.currentPage, pagination.itemsPerPage, '0', this.pagingParams)
      .subscribe((res: PaginatedResult<Student[]>) => {
        this.loading = false;
        this.pagination = res.pagination;
        this.dataSet = res.result;
      });
  }

  addNew(data: Student) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Thêm mới học sinh',
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
      nzTitle: 'Chỉnh sửa học sinh',
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
      this.studentService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }
}
