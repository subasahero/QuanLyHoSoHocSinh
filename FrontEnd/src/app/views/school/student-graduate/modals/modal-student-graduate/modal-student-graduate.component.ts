import { Component, OnInit, Input } from '@angular/core';
import { StudentService } from 'src/app/shared/services/student.service';
import { NotifyService } from 'src/app/shared/services/notify.service';

@Component({
  selector: 'app-modal-student-graduate',
  templateUrl: './modal-student-graduate.component.html',
  styleUrls: ['./modal-student-graduate.component.css']
})
export class ModalStudentGraduateComponent implements OnInit {
  @Input() student: any;
  studentData: any;
  isSpinning: boolean;
  constructor(
    private studentService: StudentService,
    private notifyService: NotifyService,
  ) { }

  ngOnInit() {
    this.getDataStudent();
  }

  getDataStudent() {
    this.isSpinning = true;
    this.studentService.GetByIdAllInfo(this.student.id).subscribe(
      (res: any) => {
        this.studentData = res;
        this.isSpinning = false;
      },
      err => {
        console.log(err);
        this.isSpinning = false;
      }
    );
  }

}
