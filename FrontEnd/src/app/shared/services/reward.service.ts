import { EnvService } from './../../env.service';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../models/pagination.model';
import { PagingParams } from '../params/paging.param';
import { Observable } from 'rxjs';
import { Reward } from '../models/reward.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RewardService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
    ) { }

  getAll() {
    return this.http.get(this.baseUrl + 'reward');
  }
  
  getCheckExistsRecord(number: string,description: string) {
    return this.http.get(this.baseUrl + `reward/CheckExistsRecord/${number}/${description}`);
  }

  getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams): Observable<PaginatedResult<Reward[]>> {
    const paginatedResult = new PaginatedResult<Reward[]>();

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

    return this.http.get<Reward[]>(this.baseUrl + 'reward/getAllPaging', {observe: 'response', params})
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
    return this.http.get(this.baseUrl + 'reward/' + id);
  }

  addNew(data: Reward) {
    return this.http.post(this.baseUrl + 'reward', data);
  }

  update(data: Reward) {
    return this.http.put(this.baseUrl + 'reward', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'reward/' + id);
  }
}
