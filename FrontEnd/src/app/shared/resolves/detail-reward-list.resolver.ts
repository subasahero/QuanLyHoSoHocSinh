import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { ConfigConstant } from '../constants/config.constant';
import { DetailReward } from '../models/detailreward.model';
import { DetailRewardService } from '../services/detail-reward.service';


@Injectable()
export class DetailRewardListResolver implements Resolve<PaginatedResult<DetailReward[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private detailRewardService: DetailRewardService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<DetailReward[]>> {
        return this.detailRewardService.getAllPaging(this.pageNumber, this.pageSize);
    }
}
