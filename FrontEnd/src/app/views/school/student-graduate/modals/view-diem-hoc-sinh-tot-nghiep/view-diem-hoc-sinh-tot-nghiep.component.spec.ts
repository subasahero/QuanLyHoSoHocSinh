/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ViewDiemHocSinhTotNghiepComponent } from './view-diem-hoc-sinh-tot-nghiep.component';

describe('ViewDiemHocSinhTotNghiepComponent', () => {
  let component: ViewDiemHocSinhTotNghiepComponent;
  let fixture: ComponentFixture<ViewDiemHocSinhTotNghiepComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewDiemHocSinhTotNghiepComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewDiemHocSinhTotNghiepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
