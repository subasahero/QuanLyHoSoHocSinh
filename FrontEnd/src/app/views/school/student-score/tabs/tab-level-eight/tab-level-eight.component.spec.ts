/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TabLevelEightComponent } from './tab-level-eight.component';

describe('TabLevelEightComponent', () => {
  let component: TabLevelEightComponent;
  let fixture: ComponentFixture<TabLevelEightComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabLevelEightComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabLevelEightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
