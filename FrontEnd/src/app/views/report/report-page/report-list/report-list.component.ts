import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReportEnrollment } from './../../../../shared/models/report-enrollment';
import { StudentService } from './../../../../shared/services/student.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css']
})
export class ReportListComponent implements OnInit {
  currentYear = (new Date()).getFullYear();
  enrollmentForm: FormGroup;
  // chart
  public primaryXAxis: Object;
  public chartData: Object[];
  public title: string;
  public primaryYAxis: Object;
  constructor(
    private studentService: StudentService,
    private fb: FormBuilder
    ) { }

  ngOnInit() {
    this.getListReportEnrollment((this.currentYear - 5),this.currentYear);
    this.createForm();
    this.primaryXAxis = {
      valueType: 'Category',
      title: 'Năm'
    };
    this.primaryYAxis = {
      interval: 10, title: 'Số lượng học sinh'
    };
    this.title = 'Báo cáo tuyển sinh theo năm';
  }

  getListReportEnrollment(from: number, to: number) {
    this.studentService.getReportEnrollment(from.toString(), to.toString())
      .subscribe(
        (res: ReportEnrollment[]) => {
          this.chartData = res;
        }
      );
  }

  createForm() {
    this.enrollmentForm = this.fb.group({
      fromYear: [null, [Validators.required]],
      toYear: [null, [Validators.required]],
    });
  }

  saveChanges(): void {
    if(this.enrollmentForm.invalid) {
      for (const i in this.enrollmentForm.controls) {
        this.enrollmentForm.controls[i].markAsDirty();
        this.enrollmentForm.controls[i].updateValueAndValidity();
      }
    }else{
      const data = this.enrollmentForm.getRawValue();
      const fromYear: number = data.fromYear.getFullYear();
      const toYear: number = data.toYear.getFullYear();
      this.getListReportEnrollment(fromYear,toYear);
    }
  }
}
