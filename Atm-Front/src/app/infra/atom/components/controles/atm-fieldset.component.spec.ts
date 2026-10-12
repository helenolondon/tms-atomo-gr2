import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtmFieldsetComponent } from './atm-fieldset.component';

describe('AtmFieldsetComponent', () => {
  let component: AtmFieldsetComponent;
  let fixture: ComponentFixture<AtmFieldsetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtmFieldsetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtmFieldsetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
