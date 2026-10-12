import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtomServerErrorComponent } from './atom-server-error.component';

describe('AtomServerErrorComponent', () => {
  let component: AtomServerErrorComponent;
  let fixture: ComponentFixture<AtomServerErrorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtomServerErrorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtomServerErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
