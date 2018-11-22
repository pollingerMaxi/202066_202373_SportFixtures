import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FixtureGeneratorComponent } from './fixture-generator.component';

describe('FixtureGeneratorComponent', () => {
  let component: FixtureGeneratorComponent;
  let fixture: ComponentFixture<FixtureGeneratorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FixtureGeneratorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FixtureGeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
