import { EnvService } from './../../env.service';
import { environment } from './../../../environments/environment.prod';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StudentScore } from '../models/student-score.model';

@Injectable({
  providedIn: 'root'
})
export class StudentScoreService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
    ) { }

  getDetail(id: any) {
    return this.http.get(this.baseUrl + 'StudentScore/' + id);
  }

  addNew(data: StudentScore) {
    return this.http.post(this.baseUrl + 'StudentScore', data);
  }

  update(data: StudentScore) {
    return this.http.put(this.baseUrl + 'StudentScore', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'StudentScore/' + id);
  }
}
