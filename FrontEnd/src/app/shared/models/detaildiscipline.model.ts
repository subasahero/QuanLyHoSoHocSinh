import { Student } from './student.model';
import { Discipline } from './discipline.model';

export interface DetailDiscipline {
    id?: string;
    studentId?: string;
    disciplineId?: string;
    reason?: string;
    punishment?: string;
    datePunish?: string;
    createdDate?: string;
    modifiedDate?: string;
    status?: boolean;
    studentVM?: Student;
    disciplineVM?: Discipline;
}
