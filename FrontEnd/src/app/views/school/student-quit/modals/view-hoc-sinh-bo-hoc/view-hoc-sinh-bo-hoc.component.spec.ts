/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ViewHocSinhBoHocComponent } from './view-hoc-sinh-bo-hoc.component';

describe('ViewHocSinhBoHocComponent', () => {
  let component: ViewHocSinhBoHocComponent;
  let fixture: ComponentFixture<ViewHocSinhBoHocComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewHocSinhBoHocComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewHocSinhBoHocComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
