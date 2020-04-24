import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EnvService } from 'src/app/env.service';

@Injectable({
  providedIn: 'root'
})
export class DiemlopbayService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
  ) { }

  getDetail(id: any) {
    return this.http.get(this.baseUrl + 'DiemLopBay/' + id);
  }

  addNew(data: any) {
    return this.http.post(this.baseUrl + 'DiemLopBay', data);
  }

  update(data: any) {
    return this.http.put(this.baseUrl + 'DiemLopBay', data);
  }
}
