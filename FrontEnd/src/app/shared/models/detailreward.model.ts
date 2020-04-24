import { Student } from './student.model';
import { Reward } from './reward.model';

export interface DetailReward {
    id?: string;
    studentId?: string;
    rewardId?: string;
    reason?: string;
    gift?: string;
    dateReward?: string;
    createdDate?: string;
    modifiedDate?: string;
    status?: boolean;
    studentVM: Student;
    reward: any;
}
