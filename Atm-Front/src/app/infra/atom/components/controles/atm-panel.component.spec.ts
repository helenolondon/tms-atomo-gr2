import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtmPanelComponent } from './atm-panel.component';

describe('AtmPanelComponent', () => {
  let component: AtmPanelComponent;
  let fixture: ComponentFixture<AtmPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtmPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtmPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
