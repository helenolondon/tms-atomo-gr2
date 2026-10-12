import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BotaoPagamentoComponent } from './botao-pagamento.component';

describe('BotaoPagamentoComponent', () => {
  let component: BotaoPagamentoComponent;
  let fixture: ComponentFixture<BotaoPagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BotaoPagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BotaoPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
