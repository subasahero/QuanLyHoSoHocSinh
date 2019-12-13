import { LevelsServiceService } from 'src/app/shared/services/levels-service.service';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { Level } from '../models/level.model';
import { ConfigConstant } from '../constants/config.constant';


@Injectable()
export class LevelListResolver implements Resolve<PaginatedResult<Level[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private levelService: LevelsServiceService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Level[]>> {
        return this.levelService.getAllPaging(this.pageNumber, this.pageSize);
    }
}
