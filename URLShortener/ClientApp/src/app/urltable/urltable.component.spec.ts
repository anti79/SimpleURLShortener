import { ComponentFixture, TestBed } from '@angular/core/testing';

import { URLTableComponent } from './urltable.component';

describe('URLTableComponent', () => {
  let component: URLTableComponent;
  let fixture: ComponentFixture<URLTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ URLTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(URLTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
