import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import { Pagination, PaginatedResult } from 'src/app/shared/models/pagination.model';
import { PagingParams } from 'src/app/shared/params/paging.param';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { NzDrawerService } from 'ng-zorro-antd';
import { CreateEditUserComponent } from '../modals/create-edit-user/create-edit-user.component';
import { ConfigMesageConstant } from 'src/app/shared/constants/configmessage.constant';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  dataSet = [];
  loading = true;
  pagination: Pagination = {};
  pagingParams: PagingParams = {
    pageNumber: 1,
    pageSize: 10,
    keyword: '',
    sortKey: '',
    searchKey: '',
    searchValue: '',
    levelIdValue: '',
  };

  constructor(
    private userService: UserService,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData(reset: boolean = false) {
    this.loading = true;
    if (reset) {
      this.pagination.currentPage = 1;
    }
    this.loading = true;
    this.userService.getAllPaging(1, 10, this.pagingParams)
      .subscribe((res: any) => {
        this.loading = false;
        this.pagination = res.pagination;
        this.dataSet = res.result;
        this.loading = false;
        console.log(this.dataSet);
      });
  }

  search(keyword: string) {
    this.pagingParams.keyword = keyword;
    this.loadData();
  }

  addNew() {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Thêm mới người dùng',
      nzContent: CreateEditUserComponent,
      nzWidth: 400,
      nzContentParams: {
        userData: {},
        isAddNew: true
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Chỉnh sửa thông tin',
      nzContent: CreateEditUserComponent,
      nzWidth: 400,
      nzContentParams: {
        userData: data,
        isAddNew: false
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  delete(id: any) {
    this.notifyService.confirm('Bạn có chắc muốn xóa không?', () => {
      this.userService.delete(id).subscribe((res: boolean) => {
        if (res) {
          this.notifyService.success(ConfigMesageConstant.MESSAGE_DELETE_SUCCESS_MODAL);
          this.loadData();
        }
      });
    }, 'Đồng ý', 'Hủy bỏ', () => false);
  }

}
