/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ViewDiemHocSinhBoHocComponent } from './view-diem-hoc-sinh-bo-hoc.component';

describe('ViewDiemHocSinhBoHocComponent', () => {
  let component: ViewDiemHocSinhBoHocComponent;
  let fixture: ComponentFixture<ViewDiemHocSinhBoHocComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewDiemHocSinhBoHocComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewDiemHocSinhBoHocComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
