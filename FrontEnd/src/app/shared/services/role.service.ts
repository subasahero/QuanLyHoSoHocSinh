import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EnvService } from 'src/app/env.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
  ) { }

  GetAll() {
    return this.http.get(this.baseUrl + 'Role');
  }
}
