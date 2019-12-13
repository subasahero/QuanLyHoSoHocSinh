import { PagingParams } from './../params/paging.param';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { map } from 'rxjs/operators';
import { Grade } from '../models/grade.model';

@Injectable({
  providedIn: 'root'
})
export class GradesService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl + 'grade');
  }
  
  getCheckExistsRecord(name: string, levelId: any) {
    return this.http.get(this.baseUrl + `grade/CheckExistsRecord/${name}/${levelId}`);
  }

  getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams): Observable<PaginatedResult<Grade[]>> {
    const paginatedResult = new PaginatedResult<Grade[]>();

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

    return this.http.get<Grade[]>(this.baseUrl + 'grade/getAllPaging', {observe: 'response', params})
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
    return this.http.get(this.baseUrl + 'Grade/' + id);
  }

  addNew(data: Grade) {
    return this.http.post(this.baseUrl + 'Grade', data);
  }

  update(data: Grade) {
    return this.http.put(this.baseUrl + 'Grade', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'Grade/' + id);
  }
}
