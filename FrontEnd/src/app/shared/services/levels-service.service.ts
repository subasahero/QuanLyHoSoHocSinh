import { PagingParams } from './../params/paging.param';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Level } from '../models/level.model';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LevelsServiceService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl + 'level');
  }

  checkExistsName(name: any) {
    return this.http.get(this.baseUrl + 'Level/CheckExistsName/' + name);
  }

  getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams): Observable<PaginatedResult<Level[]>> {
    const paginatedResult = new PaginatedResult<Level[]>();

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
    }

    return this.http.get<Level[]>(this.baseUrl + 'level/getAllPaging', {observe: 'response', params})
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
    return this.http.get(this.baseUrl + 'level/' + id);
  }

  addNew(level: Level) {
    return this.http.post(this.baseUrl + 'level', level);
  }

  update(level: Level) {
    return this.http.put(this.baseUrl + 'level', level);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'level/' + id);
  }
}

