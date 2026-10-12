import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtmButtonSucessComponent } from './atm-button-sucess.component';

describe('AtmButtonSucessComponent', () => {
  let component: AtmButtonSucessComponent;
  let fixture: ComponentFixture<AtmButtonSucessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtmButtonSucessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtmButtonSucessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
