import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtmMaskEditorComponent } from './atm-mask-editor.component';

describe('AtmMaskEditorComponent', () => {
  let component: AtmMaskEditorComponent;
  let fixture: ComponentFixture<AtmMaskEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtmMaskEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtmMaskEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
