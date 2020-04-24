import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-diem-hoc-sinh-bo-hoc',
  templateUrl: './view-diem-hoc-sinh-bo-hoc.component.html',
  styleUrls: ['./view-diem-hoc-sinh-bo-hoc.component.css']
})
export class ViewDiemHocSinhBoHocComponent implements OnInit {
  @Input() diemData: any;
  constructor() { }

  ngOnInit() {
  }

}
