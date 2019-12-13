import { Grade } from './grade.model';
import { Level } from './level.model';
export interface Student {
    id?: string;
    gradeId?: string;
    code?: string;
    name?: string;
    sex?: string;
    birthday?: string;
    birthLocate?: string;
    talent?: string;
    dateGoShcool?: string;
    createdDate?: string;
    modifiedDate?: string;
    status?: boolean;
    gradeVM?: Grade;
}
