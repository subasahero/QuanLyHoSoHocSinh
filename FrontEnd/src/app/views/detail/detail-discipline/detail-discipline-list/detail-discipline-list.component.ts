import { ConfigMesageConstant } from './../../../../shared/constants/configmessage.constant';
import { DetailDiscipline } from './../../../../shared/models/detaildiscipline.model';
import { NotifyService } from './../../../../shared/services/notify.service';
import { DetailDisciplineService } from './../../../../shared/services/detail-discipline.service';
import { PagingParams } from './../../../../shared/params/paging.param';
import { Pagination, PaginatedResult } from './../../../../shared/models/pagination.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService } from 'ng-zorro-antd';
import { DetailDisciplineCreateEditModalComponent } from '../modal/detail-discipline-create-edit-modal/detail-discipline-create-edit-modal.component';

@Component({
  selector: 'app-detail-discipline-list',
  templateUrl: './detail-discipline-list.component.html',
  styleUrls: ['./detail-discipline-list.component.css']
})
export class DetailDisciplineListComponent implements OnInit {
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
    private detailDisciplineService: DetailDisciplineService,
    private route: ActivatedRoute,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.loading = false;
      this.pagination = data['detail-discipline-list'].pagination;
      this.dataSet = data['detail-discipline-list'].result;
      console.log(this.dataSet);
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
    this.detailDisciplineService.getAllPaging(this.pagination.currentPage, this.pagination.itemsPerPage, this.pagingParams)
      .subscribe((res: PaginatedResult<DetailDiscipline[]>) => {
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
      nzTitle: 'Thêm mới học sinh bị kỷ luật',
      nzContent: DetailDisciplineCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        detailDiscipline: {},
        isAddNew: true
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data: DetailDiscipline) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Chỉnh sửa kỷ luật',
      nzContent: DetailDisciplineCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        detailDiscipline: data,
        isAddNew: false
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  delete(id: any) {
    this.notifyService.confirm('Bạn có chắc muốn xóa không?', () => {
      this.detailDisciplineService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }
}
