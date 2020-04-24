import { Component, OnInit } from '@angular/core';
import { ConfigConstant } from 'src/app/shared/constants/config.constant';
import { Pagination, PaginatedResult } from 'src/app/shared/models/pagination.model';
import { PagingParams } from 'src/app/shared/params/paging.param';
import { StudentService } from 'src/app/shared/services/student.service';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { NzDrawerService, NzModalService } from 'ng-zorro-antd';
import { StudentScoreService } from 'src/app/shared/services/student-score.service.ts.service';
import { GradesService } from 'src/app/shared/services/grades.service';
import { ModalStudentGraduateComponent } from '../../student-graduate/modals/modal-student-graduate/modal-student-graduate.component';
import { ViewHocSinhBoHocComponent } from '../modals/view-hoc-sinh-bo-hoc/view-hoc-sinh-bo-hoc.component';

@Component({
  selector: 'app-student-quit-list',
  templateUrl: './student-quit-list.component.html',
  styleUrls: ['./student-quit-list.component.css']
})
export class StudentQuitListComponent implements OnInit {
  pageNumber = ConfigConstant.PAGE_INDEX;
  pageSize = ConfigConstant.PAGE_SIZE;
  selectedValue: any;
  dataSet = [];
  loading = true;
  sortValue = null;
  sortKey = null;
  listLopBoHoc: any[] = [];
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
    private studentScoreService: StudentScoreService,
    private gradesService: GradesService
  ) { }
  ngOnInit() {
    this.getListLopBoHoc(5);
    this.loading = true;
    this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '5').subscribe(
      (res: PaginatedResult<any[]>) => {
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
    this.studentService.GetStudentByLevelPaging(pagination.currentPage, pagination.itemsPerPage, '5', this.pagingParams)
      .subscribe((res: PaginatedResult<any[]>) => {
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
    this.loadData();
  }

  getListLopBoHoc(level: any) {
    return this.gradesService.GetGradeByLevel(level).subscribe(
      (res: any) => {
        this.listLopBoHoc = res;
        console.log(this.listLopBoHoc);
      },
      err => {
        console.log(err);
      }
    );
  }

  view(data) {
    const drawerRef = this.drawerService.create({
      nzContent: ViewHocSinhBoHocComponent,
      nzWidth: "60%",
      nzContentParams: {
        student: data,
      }
    });
    drawerRef.afterClose.subscribe(() => {
      this.loadData();
    });
  }

  changeKhoaBoHoc(data) {
    if(data === null) {
      this.pagingParams.gradeId = '';
    } else {
      this.pagingParams.gradeId = data;
    }
    this.loadData(true);
  }

}
