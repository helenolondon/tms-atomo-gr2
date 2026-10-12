import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrarSeComponent } from './registrar-se.component';

describe('RegistrarSeComponent', () => {
  let component: RegistrarSeComponent;
  let fixture: ComponentFixture<RegistrarSeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrarSeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrarSeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
