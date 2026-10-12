import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtmServerResponseErrorComponent } from './atm-server-response-error.component';

describe('AtmServerResponseErrorComponent', () => {
  let component: AtmServerResponseErrorComponent;
  let fixture: ComponentFixture<AtmServerResponseErrorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtmServerResponseErrorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtmServerResponseErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
