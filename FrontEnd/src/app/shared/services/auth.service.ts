import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EnvService } from 'src/app/env.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = this.env.apiUrl;
  constructor(
    private http: HttpClient,
    private env: EnvService
    ) { }

  login(data: any) {
    return this.http.post(this.baseUrl + 'Auth/Login', data);
  }
}
