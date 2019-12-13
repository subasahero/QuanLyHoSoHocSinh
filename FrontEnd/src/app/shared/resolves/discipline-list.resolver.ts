import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { ConfigConstant } from '../constants/config.constant';
import { DisciplineService } from '../services/discipline.service';
import { Discipline } from '../models/discipline.model';


@Injectable()
export class DisciplineListResolver implements Resolve<PaginatedResult<Discipline[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private disciplineService: DisciplineService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Discipline[]>> {
        return this.disciplineService.getAllPaging(this.pageNumber, this.pageSize);
    }
}
