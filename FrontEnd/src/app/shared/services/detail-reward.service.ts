import { EnvService } from './../../env.service';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagingParams } from '../params/paging.param';
import { PaginatedResult } from '../models/pagination.model';
import { DetailReward } from '../models/detailreward.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DetailRewardService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
    ) { }

  getAll() {
    return this.http.get(this.baseUrl + 'DetailReward');
  }

  getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams): Observable<PaginatedResult<DetailReward[]>> {
    const paginatedResult = new PaginatedResult<DetailReward[]>();

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

    return this.http.get<DetailReward[]>(this.baseUrl + 'DetailReward/getAllPaging', {observe: 'response', params})
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
    return this.http.get(this.baseUrl + 'DetailReward/' + id);
  }

  addNew(data: DetailReward) {
    return this.http.post(this.baseUrl + 'DetailReward', data);
  }

  update(data: DetailReward) {
    return this.http.put(this.baseUrl + 'DetailReward', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'DetailReward/' + id);
  }
}
