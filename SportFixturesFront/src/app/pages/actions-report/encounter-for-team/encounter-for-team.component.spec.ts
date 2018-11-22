import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EncounterForTeamComponent } from './encounter-for-team.component';

describe('EncounterForTeamComponent', () => {
  let component: EncounterForTeamComponent;
  let fixture: ComponentFixture<EncounterForTeamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EncounterForTeamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EncounterForTeamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
