import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination.model';
import { ConfigConstant } from '../constants/config.constant';
import { Reward } from '../models/reward.model';
import { RewardService } from '../services/reward.service';


@Injectable()
export class RewardListResolver implements Resolve<PaginatedResult<Reward[]>> {
    pageNumber = ConfigConstant.PAGE_INDEX;
    pageSize = ConfigConstant.PAGE_SIZE;

    constructor(private rewardService: RewardService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Reward[]>> {
        return this.rewardService.getAllPaging(this.pageNumber, this.pageSize);
    }
}
