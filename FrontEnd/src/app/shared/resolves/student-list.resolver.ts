import { PaginatedResult } from './../models/pagination.model';
import { Student } from '../models/student.model';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { StudentService } from '../services/student.service';
import { ConfigConstant } from '../constants/config.constant';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';


@Injectable()
export class StudentListResolver implements Resolve<PaginatedResult<Student[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private studentService: StudentService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Student[]>> {
        return this.studentService.getAllPaging(this.pageNumber, this.pageSize);
    }
}
