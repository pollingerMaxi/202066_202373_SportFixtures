import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SportsManagementComponent } from './sports-management.component';

describe('SportsManagementComponent', () => {
  let component: SportsManagementComponent;
  let fixture: ComponentFixture<SportsManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SportsManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SportsManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
