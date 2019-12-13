import { ConfigMesageConstant } from './../../../../shared/constants/configmessage.constant';
import { NotifyService } from './../../../../shared/services/notify.service';
import { DisciplineService } from './../../../../shared/services/discipline.service';
import { PagingParams } from './../../../../shared/params/paging.param';
import { Pagination, PaginatedResult } from 'src/app/shared/models/pagination.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService } from 'ng-zorro-antd';
import { Discipline } from 'src/app/shared/models/discipline.model';
import { DisciplineCreateEditModalComponent } from '../modal/discipline-create-edit-modal/discipline-create-edit-modal.component';

@Component({
  selector: 'app-discipline-list',
  templateUrl: './discipline-list.component.html',
  styleUrls: ['./discipline-list.component.css']
})
export class DisciplineListComponent implements OnInit {
  dataSet = [];
  loading = true;
  sortValue = null;
  sortKey = null;
  pagination: Pagination;
  pagingParams: PagingParams = {
    keyword: '',
    sortKey: '',
    searchKey: '',
    searchValue: '',
    levelIdValue: '',
  };

  constructor(
    private disciplineService: DisciplineService,
    private route: ActivatedRoute,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.loading = false;
      this.pagination = data['discipline-list'].pagination;
      this.dataSet = data['discipline-list'].result;
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
    this.disciplineService.getAllPaging(this.pagination.currentPage, this.pagination.itemsPerPage, this.pagingParams)
      .subscribe((res: PaginatedResult<Discipline[]>) => {
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
      nzTitle: 'Thêm mới kỷ luật',
      nzContent: DisciplineCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        discipline: {},
        isAddNew: true
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data: Discipline) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Chỉnh sửa kỷ luật',
      nzContent: DisciplineCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        discipline: data,
        isAddNew: false
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  delete(id: any) {
    this.notifyService.confirm('Bạn có chắc muốn xóa không?', () => {
      this.disciplineService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }
}
