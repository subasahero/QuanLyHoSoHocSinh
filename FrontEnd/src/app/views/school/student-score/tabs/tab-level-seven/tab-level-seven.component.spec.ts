/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TabLevelSevenComponent } from './tab-level-seven.component';

describe('TabLevelSevenComponent', () => {
  let component: TabLevelSevenComponent;
  let fixture: ComponentFixture<TabLevelSevenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabLevelSevenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabLevelSevenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
