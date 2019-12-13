import { ConfigMesageConstant } from './../../../../shared/constants/configmessage.constant';
import { Grade } from './../../../../shared/models/grade.model';
import { NotifyService } from './../../../../shared/services/notify.service';
import { PagingParams } from './../../../../shared/params/paging.param';
import { Pagination, PaginatedResult } from './../../../../shared/models/pagination.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService } from 'ng-zorro-antd';
import { GradesService } from 'src/app/shared/services/grades.service';
import { GradeCreateEditModalComponent } from '../modal/grade-create-edit-modal/grade-create-edit-modal.component';

@Component({
  selector: 'app-grade-list',
  templateUrl: './grade-list.component.html',
  styleUrls: ['./grade-list.component.css']
})
export class GradeListComponent implements OnInit {
  dataSet = [];
  loading = true;
  sortValue = null;
  sortKey = null;
  listNameLevel = ['Khối 6','Khối 7','Khối 8','Khối 9','Tốt nghiệp','Bỏ học','Chuyển trường'];
  pagination: Pagination;
  pagingParams: PagingParams = {
    keyword: '',
    sortKey: '',
    searchKey: '',
    searchValue: '',
    levelIdValue: '',
  };

  constructor(
    private gradeService: GradesService,
    private route: ActivatedRoute,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.loading = false;
      this.pagination = data['grade-list'].pagination;
      this.dataSet = data['grade-list'].result;
    });
  }

  sort(sort: { key: string, value: string }): void {
    this.pagingParams.sortKey = sort.key;
    this.pagingParams.sortValue = sort.value;
    this.loadData();
  }

  loadData(reset: boolean = false) {
    if (reset) {
      this.pagination.currentPage = 1; 
    }
    this.loading = true;
    this.gradeService.getAllPaging(this.pagination.currentPage, this.pagination.itemsPerPage, this.pagingParams)
      .subscribe((res: PaginatedResult<Grade[]>) => {
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
      nzTitle: 'Thêm mới lớp',
      nzContent: GradeCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        grade: {},
        isAddNew: true
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data: Grade) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Chỉnh sửa khối',
      nzContent: GradeCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        grade: data,
        isAddNew: false
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  delete(id: any) {
    this.notifyService.confirm('Bạn có chắc muốn xóa không?', () => {
      this.gradeService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }
}
