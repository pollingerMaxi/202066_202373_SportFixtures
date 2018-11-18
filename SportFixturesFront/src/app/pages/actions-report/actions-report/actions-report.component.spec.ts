import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionsReportComponent } from './actions-report.component';

describe('ActionsReportComponent', () => {
  let component: ActionsReportComponent;
  let fixture: ComponentFixture<ActionsReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActionsReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActionsReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
