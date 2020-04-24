import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-diem-theo-lop',
  templateUrl: './view-diem-theo-lop.component.html',
  styleUrls: ['./view-diem-theo-lop.component.css']
})
export class ViewDiemTheoLopComponent implements OnInit {
  @Input() diemData: any;
  constructor() { }

  ngOnInit() {
  }

}
