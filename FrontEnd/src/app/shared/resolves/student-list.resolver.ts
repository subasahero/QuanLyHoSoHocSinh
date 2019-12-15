import { PaginatedResult } from './../models/pagination.model';
import { Student } from '../models/student.model';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { StudentService } from '../services/student.service';
import { ConfigConstant } from '../constants/config.constant';
import { Observable, forkJoin } from 'rxjs';
import { Injectable } from '@angular/core';


@Injectable()
export class StudentListResolver implements Resolve<PaginatedResult<Student[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private studentService: StudentService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return forkJoin([
            this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '0'),
            this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '1'),
            this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '2'),
            this.studentService.GetStudentByLevelPaging(this.pageNumber, this.pageSize, '3'),
        ]);
    }
}
