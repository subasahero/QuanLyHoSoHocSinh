import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../models/pagination.model';
import { Discipline } from '../models/discipline.model';
import { PagingParams } from '../params/paging.param';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DisciplineService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl + 'Discipline');
  }
  getCheckExistsRecord(numberCode: string, description: string) {
    return this.http.get(this.baseUrl + `Discipline/CheckExistsRecord/${numberCode}/${description}`);
  }

  getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams): Observable<PaginatedResult<Discipline[]>> {
    const paginatedResult = new PaginatedResult<Discipline[]>();

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

    return this.http.get<Discipline[]>(this.baseUrl + 'discipline/getAllPaging', {observe: 'response', params})
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
    return this.http.get(this.baseUrl + 'Discipline/' + id);
  }

  addNew(data: Discipline) {
    return this.http.post(this.baseUrl + 'Discipline', data);
  }

  update(data: Discipline) {
    return this.http.put(this.baseUrl + 'Discipline', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'Discipline/' + id);
  }
}
