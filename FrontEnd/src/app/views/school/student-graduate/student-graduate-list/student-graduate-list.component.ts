import { Student } from './../../../../shared/models/student.model';
import { PaginatedResult } from './../../../../shared/models/pagination.model';
import { StudentScoreService } from './../../../../shared/services/student-score.service.ts.service';
import { StudentService } from './../../../../shared/services/student.service';
import { ConfigConstant } from './../../../../shared/constants/config.constant';
import { NotifyService } from './../../../../shared/services/notify.service';
import { LevelsServiceService } from './../../../../shared/services/levels-service.service';
import { PagingParams } from './../../../../shared/params/paging.param';
import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/shared/models/pagination.model';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzDrawerService, NzModalService } from 'ng-zorro-antd';
import { GradesService } from 'src/app/shared/services/grades.service';

@Component({
  selector: 'app-student-graduate-list',
  templateUrl: './student-graduate-list.component.html',
  styleUrls: ['./student-graduate-list.component.css']
})
export class StudentGraduateListComponent implements OnInit {
  pageNumber = ConfigConstant.PAGE_INDEX;
  pageSize = ConfigConstant.PAGE_SIZE;
  dataSet = [];
  loading = true;
  sortValue = null;
  sortKey = null;
  listLopTotNghiep: any[] = [];
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
    this.getListLopTotNghiep(4);
    this.loading = true;
    this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '4').subscribe(
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
    this.studentService.GetStudentByLevelPaging(pagination.currentPage, pagination.itemsPerPage, '4', this.pagingParams)
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
    this.loadData();
  }

  getListLopTotNghiep(level: any) {
    return this.gradesService.GetGradeByLevel(level).subscribe(
      (res: any) => {
        this.listLopTotNghiep = res;
        console.log(this.listLopTotNghiep);
      },
      err => {
        console.log(err);
      }
    );
  }

  changeKhoaTotNghiep(data) {
    if(data === null) {
      this.pagingParams.gradeId = '';
    } else {
      this.pagingParams.gradeId = data;
    }
    this.loadData(true);
  }
}
