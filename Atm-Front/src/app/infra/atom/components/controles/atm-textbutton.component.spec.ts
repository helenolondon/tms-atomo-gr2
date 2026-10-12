import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtmTextbuttonComponent } from './atm-textbutton.component';

describe('AtmTextbuttonComponent', () => {
  let component: AtmTextbuttonComponent;
  let fixture: ComponentFixture<AtmTextbuttonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtmTextbuttonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtmTextbuttonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
