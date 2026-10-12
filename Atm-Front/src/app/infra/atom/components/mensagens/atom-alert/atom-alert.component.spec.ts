import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtomAlertComponent } from './atom-alert.component';

describe('AtomAlertComponent', () => {
  let component: AtomAlertComponent;
  let fixture: ComponentFixture<AtomAlertComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtomAlertComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtomAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
