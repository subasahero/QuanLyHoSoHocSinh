import { EnvService } from './../../env.service';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PagingParams } from '../params/paging.param';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { Student } from '../models/student.model';
import { map } from 'rxjs/operators';
import { ChangeStudentGradeModel } from '../models/change-student-grade.model';
import { ReportEnrollment } from '../models/report-enrollment';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
    ) { }

  getAll() {
    return this.http.get(this.baseUrl + 'Student');
  }

  changeGrade(data: ChangeStudentGradeModel) {
    return this.http.put(this.baseUrl + 'Student/ChangeGrade', data);
  }

  getReportEnrollment(fromYear: string, toYear: string) {
    let listReportEnrollment: ReportEnrollment[];
    let params = new HttpParams();
    params = params.append('FromYear', fromYear);
    params = params.append('ToYear', toYear);
    return this.http.get<ReportEnrollment[]>(this.baseUrl + 'Student/reportEnrollment', { observe: 'response', params })
      .pipe(
        map(response => {
          listReportEnrollment = response.body;
          return listReportEnrollment;
        })
      );
  }

  getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams): Observable<PaginatedResult<Student[]>> {
    const paginatedResult = new PaginatedResult<Student[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (pagingParams != null) {
      params = params.append('keyword', pagingParams.keyword);
      params = params.append('sortKey', pagingParams.sortKey);
      params = params.append('sortValue', pagingParams.sortValue);
      params = params.append('searchKey', pagingParams.searchKey);
      params = params.append('searchValue', pagingParams.searchValue);
      params = params.append('levelIdValue', pagingParams.levelIdValue);
    }

    return this.http.get<Student[]>(this.baseUrl + 'Student/getAllPaging', { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  // tslint:disable-next-line: max-line-length
  GetStudentByLevelPaging(page?: any, itemsPerPage?: any, levelEnum?: any, pagingParams?: PagingParams): Observable<PaginatedResult<Student[]>> {
    const paginatedResult = new PaginatedResult<Student[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (pagingParams != null) {
      params = params.append('keyword', pagingParams.keyword);
      params = params.append('sortKey', pagingParams.sortKey);
      params = params.append('sortValue', pagingParams.sortValue);
      params = params.append('searchKey', pagingParams.searchKey);
      params = params.append('searchValue', pagingParams.searchValue);
      params = params.append('levelIdValue', pagingParams.levelIdValue);
      params = params.append('gradeId', pagingParams.gradeId);
    }

    return this.http.get<Student[]>(this.baseUrl + 'Student/GetStudentByLevelPaging/' + levelEnum, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getDetail(id: any) {
    return this.http.get(this.baseUrl + 'Student/' + id);
  }

  GetByIdAllInfo(id: any) {
    return this.http.get(this.baseUrl + 'Student/GetByIdAllInfo/' + id);
  }

  addNew(data: any) {
    return this.http.post(this.baseUrl + 'Student', data);
  }

  update(data: any) {
    return this.http.put(this.baseUrl + 'Student', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'Student/' + id);
  }
}
