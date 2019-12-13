import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { Level } from '../models/level.model';
import { ConfigConstant } from '../constants/config.constant';
import { GradesService } from '../services/grades.service';


@Injectable()
export class GradeListResolver implements Resolve<PaginatedResult<Level[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private gradeService: GradesService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Level[]>> {
        return this.gradeService.getAllPaging(this.pageNumber, this.pageSize);
    }
}
