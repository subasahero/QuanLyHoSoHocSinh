import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { EnvService } from 'src/app/env.service';
import { PagingParams } from '../params/paging.param';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../models/pagination.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
    ) { }

    getAllPaging(page?: any, itemsPerPage?: any, pagingParams?: PagingParams) {
      const paginatedResult = new PaginatedResult();
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
  
      return this.http.get(this.baseUrl + 'User/getAllPaging', { observe: 'response', params })
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

    GetAll() {
      return this.http.get(this.baseUrl + 'User');
    }

    GetById(id: any) {
      return this.http.get(this.baseUrl + 'User/GetById/' + id);
    }
  
    addNew(data: any) {
      return this.http.post(this.baseUrl + 'User', data);
    }
  
    update(data: any) {
      return this.http.put(this.baseUrl + 'User', data);
    }

    changePasswordForUser(data: any) {
      return this.http.put(this.baseUrl + 'User/changePasswordForUser', data);
    }
  
    delete(id: any) {
      return this.http.delete(this.baseUrl + 'User/' + id);
    }
  
}
