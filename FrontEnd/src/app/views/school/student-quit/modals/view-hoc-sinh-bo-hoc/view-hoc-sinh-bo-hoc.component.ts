import { Component, OnInit, Input } from '@angular/core';
import { StudentService } from 'src/app/shared/services/student.service';
import { NotifyService } from 'src/app/shared/services/notify.service';
import { NzDrawerService } from 'ng-zorro-antd';
import { EnvService } from 'src/app/env.service';
import { ViewDiemHocSinhBoHocComponent } from '../view-diem-hoc-sinh-bo-hoc/view-diem-hoc-sinh-bo-hoc.component';

@Component({
  selector: 'app-view-hoc-sinh-bo-hoc',
  templateUrl: './view-hoc-sinh-bo-hoc.component.html',
  styleUrls: ['./view-hoc-sinh-bo-hoc.component.css']
})
export class ViewHocSinhBoHocComponent implements OnInit {
  @Input() student: any;
  studentData: any;
  imageLinkStudent: any;
  isSpinning: boolean;
  loading: boolean;
  constructor(
    private studentService: StudentService,
    private notifyService: NotifyService,
    private drawerService: NzDrawerService,
    private env: EnvService
  ) { }

  ngOnInit() {
    this.getDataStudent();
  }

  getDataStudent() {
    this.isSpinning = true;
    this.studentService.GetByIdAllInfo(this.student.id).subscribe(
      (res: any) => {
        this.studentData = res;
        console.log(res);
        this.isSpinning = false;
        this.imageLinkStudent = this.env.apiImg + 'img/' + res.imageLink;
      },
      err => {
        console.log(err);
        this.isSpinning = false;
      }
    );
  }

  view61(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopSauHK1VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
      
    });
  }

  view62(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopSauHK2VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
      
    });
  }

  view71(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopBayHK1VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
      
    });
  }

  view72(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopBayHK2VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
      
    });
  }

  view81(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopTamHK1VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
      
    });
  }

  view82(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopTamHK2VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
    });
  }

  view91(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopChinHK1VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
      
    });
  }

  view92(data: any) {
    const drawerRef = this.drawerService.create({
      nzTitle: 'Xem bảng điểm chi tiết',
      nzContent: ViewDiemHocSinhBoHocComponent,
      nzWidth: 650,
      nzContentParams: {
        diemData: data.diemLopChinHK2VM,
      }
    });

    drawerRef.afterClose.subscribe(() => {
    });
  }
}
