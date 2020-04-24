import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-diem-hoc-sinh-tot-nghiep',
  templateUrl: './view-diem-hoc-sinh-tot-nghiep.component.html',
  styleUrls: ['./view-diem-hoc-sinh-tot-nghiep.component.css']
})
export class ViewDiemHocSinhTotNghiepComponent implements OnInit {
  @Input() diemData: any;
  constructor() { }

  ngOnInit() {
  }

}
