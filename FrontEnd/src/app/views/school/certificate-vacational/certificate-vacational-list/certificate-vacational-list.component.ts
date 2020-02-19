import { Student } from './../../../../shared/models/student.model';
import { NotifyService } from './../../../../shared/services/notify.service';
import { PagingParams } from './../../../../shared/params/paging.param';
import { Pagination, PaginatedResult } from './../../../../shared/models/pagination.model';
import { StudentService } from './../../../../shared/services/student.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzDrawerService } from 'ng-zorro-antd';
import { CreateEditCertificateModalComponent } from '../modals/create-edit-certificate-modal/create-edit-certificate-modal.component';

@Component({
  selector: 'app-certificate-vacational-list',
  templateUrl: './certificate-vacational-list.component.html',
  styleUrls: ['./certificate-vacational-list.component.css']
})
export class CertificateVacationalListComponent implements OnInit {
  listCareer = [
    { label: 'Nấu Ăn', value: '0' },
    { label: 'Điện dân dụng', value: '1' },
    { label: 'Thợ xây', value: '2' },
  ];
  listCertificateType = [
    { label: 'Kém', value: '0' },
    { label: 'Khá', value: '1' },
    { label: 'Giỏi', value: '2' },
  ];
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
    private studentService: StudentService,
    private route: ActivatedRoute,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.loading = false;
      this.pagination = data['certificate-vacational-list'].pagination;
      this.dataSet = data['certificate-vacational-list'].result;
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
    this.studentService.GetStudentByLevelPaging(this.pagination.currentPage, this.pagination.itemsPerPage, '3', this.pagingParams)
      .subscribe((res: PaginatedResult<Student[]>) => {
        this.loading = false;
        this.pagination = res.pagination;
        this.dataSet = res.result;
        console.log(this.dataSet);
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

  addnew(data) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Thêm mới chứng chỉ nghề',
      nzContent: CreateEditCertificateModalComponent,
      nzWidth: 400,
      nzContentParams: {
        student: data,
        certificate: {},
        isAddNew: true
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  update(data) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Cập nhật chứng chỉ nghề',
      nzContent: CreateEditCertificateModalComponent,
      nzWidth: 400,
      nzContentParams: {
        student: data,
        certificate: data.certificateVM,
        isAddNew: false
      }
    });

    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }
}
