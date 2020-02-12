import { EnvService } from './../../env.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Certificate } from '../models/certificate.model';

@Injectable({
  providedIn: 'root'
})
export class CertificateService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
    ) { }

  getDetail(id: any) {
    return this.http.get(this.baseUrl + 'Certificate/' + id);
  }

  addNew(data: Certificate) {
    return this.http.post(this.baseUrl + 'Certificate', data);
  }

  update(data: Certificate) {
    return this.http.put(this.baseUrl + 'Certificate', data);
  }

  delete(id: any) {
    return this.http.delete(this.baseUrl + 'Certificate/' + id);
  }
}
