import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EnvService } from 'src/app/env.service';

@Injectable({
  providedIn: 'root'
})
export class DiemlopchinService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
  ) { }

  getDetail(id: any) {
    return this.http.get(this.baseUrl + 'DiemLopChin/' + id);
  }

  addNew(data: any) {
    return this.http.post(this.baseUrl + 'DiemLopChin', data);
  }

  update(data: any) {
    return this.http.put(this.baseUrl + 'DiemLopChin', data);
  }
}
