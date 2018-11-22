import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EncountersManagementComponent } from './encounters-management.component';

describe('EncountersManagementComponent', () => {
  let component: EncountersManagementComponent;
  let fixture: ComponentFixture<EncountersManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EncountersManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EncountersManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
