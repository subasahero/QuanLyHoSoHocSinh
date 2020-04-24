import { Certificate } from './certificate.model';
import { Grade } from './grade.model';
import { Level } from './level.model';
import { StudentScore } from './student-score.model';
export interface Student {
    id?: string;
    gradeId?: string;
    code?: string;
    name?: string;
    sex?: string;
    birthday?: string;
    birthLocate?: string;
    talent?: string;
    certificateId?: string;
    dateGoShcool?: string;
    createdDate?: string;
    modifiedDate?: string;
    status?: boolean;
    gradeVM?: Grade;
    studentScoreVM?: StudentScore;
    certificateVM?: Certificate;
    detailRewardVM?: any[];
    detailDisciplineVM?: any[];
}
