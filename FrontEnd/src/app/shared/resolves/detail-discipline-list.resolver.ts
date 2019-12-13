import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { ConfigConstant } from '../constants/config.constant';
import { DetailDiscipline } from '../models/detaildiscipline.model';
import { DetailDisciplineService } from '../services/detail-discipline.service';


@Injectable()
export class DetailDisciplineListResolver implements Resolve<PaginatedResult<DetailDiscipline[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private detailDisciplineService: DetailDisciplineService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<DetailDiscipline[]>> {
        return this.detailDisciplineService.getAllPaging(this.pageNumber, this.pageSize);
    }
}
