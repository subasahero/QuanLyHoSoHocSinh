import { ConfigMesageConstant } from './../../../../shared/constants/configmessage.constant';
import { Level } from './../../../../shared/models/level.model';
import { NotifyService } from './../../../../shared/services/notify.service';
import { PagingParams } from './../../../../shared/params/paging.param';
import { Component, OnInit } from '@angular/core';
import { LevelsServiceService } from 'src/app/shared/services/levels-service.service';
import { Pagination, PaginatedResult } from 'src/app/shared/models/pagination.model';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService } from 'ng-zorro-antd';
import { LevelCreateEditModalComponent } from '../modal/level-create-edit-modal/level-create-edit-modal.component';
import { HttpClient } from '@angular/common/http';
import { SearchEngine } from 'src/app/shared/until/searchengine';

@Component({
  selector: 'app-level-list',
  templateUrl: './level-list.component.html',
  styleUrls: ['./level-list.component.css']
})
export class LevelListComponent implements OnInit {
  dataSet: any = [];
  dataSetTmp: any = [];
  loading = true;
  sortValue = null;
  sortKey = null;
  pagination: Pagination;
  pagingParams: PagingParams = {
    keyword: '',
    sortKey: '',
    searchKey: '',
    searchValue: '',
  };

  constructor(
    private levelService: LevelsServiceService,
    private route: ActivatedRoute,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService,
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.loading = false;
      this.pagination = data['level-list'].pagination;
      this.dataSet = data['level-list'].result;
      this.dataSetTmp = this.dataSet;
      console.log(this.dataSet);
    });
  }

  sort(sort: { key: string, value: string }): void {
    this.pagingParams.sortKey = sort.key;
    this.pagingParams.sortValue = sort.value;
    this.loadData();
  }

  loadData(reset: boolean = false) {
    // if (reset) {
    //   this.pagination.currentPage = 1;
    // }
    // this.loading = true;
    // this.levelService.getAllPaging(this.pagination.currentPage, this.pagination.itemsPerPage, this.pagingParams)
    //   .subscribe((res: PaginatedResult<Level[]>) => {
    //     this.loading = false;
    //     this.pagination = res.pagination;
    //     this.dataSet = res.result;
    //   });
    this.loading = true;
    this.levelService.getAll().subscribe(rs => {
      this.dataSet = rs;
      this.dataSetTmp = rs;
      this.loading = false;
    });
  }

  search(keyword: any) {
    const arrCondition = ['name'];
    this.dataSet = SearchEngine(this.dataSetTmp, arrCondition, keyword);
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
      nzTitle: 'Thêm mới khối',
      nzContent: LevelCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        level: {},
        isAddNew: true
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data: Level) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Chỉnh sửa khối',
      nzContent: LevelCreateEditModalComponent,
      nzWidth: 400,
      nzContentParams: {
        level: data,
        isAddNew: false
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  delete(id: any) {
    this.notifyService.confirm('Bạn có chắc muốn xóa không?', () => {
      this.levelService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }

}
