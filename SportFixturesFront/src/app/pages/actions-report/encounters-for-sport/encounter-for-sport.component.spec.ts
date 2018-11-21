import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EncounterForSportComponent } from './encounter-for-sport.component';

describe('EncounterForSportComponent', () => {
  let component: EncounterForSportComponent;
  let fixture: ComponentFixture<EncounterForSportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EncounterForSportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EncounterForSportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
