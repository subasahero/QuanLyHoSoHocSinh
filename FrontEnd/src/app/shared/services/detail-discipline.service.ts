import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DetailDiscipline } from '../models/detaildiscipline.model';
import { PaginatedResult } from '../models/pagination.model';
import { PagingParams } from '../params/paging.param';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DetailDisciplineService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl + 'DetailDiscipline');
  }

  getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams): Observable<PaginatedResult<DetailDiscipline[]>> {
    const paginatedResult = new PaginatedResult<DetailDiscipline[]>();

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

    return this.http.get<DetailDiscipline[]>(this.baseUrl + 'DetailDiscipline/getAllPaging', {observe: 'response', params})
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
    return this.http.get(this.baseUrl + 'DetailDiscipline/' + id);
  }

  addNew(data: DetailDiscipline) {
    return this.http.post(this.baseUrl + 'DetailDiscipline', data);
  }

  update(data: DetailDiscipline) {
    return this.http.put(this.baseUrl + 'DetailDiscipline', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'DetailDiscipline/' + id);
  }
}
