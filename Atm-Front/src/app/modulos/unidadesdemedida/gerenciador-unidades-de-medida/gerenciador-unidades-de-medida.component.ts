import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import {
  AtmGerenciadorComponent
} from '../../../infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { UnidadeDeMedidaODT } from '../odts/unidade-de-medida-odt';
import { UnidadesDeMedidaService } from '../services/unidades-de-medida.service';
import {
  PropriedadesUnidadeDeMedidaComponent
} from '../propriedades-unidade-de-medida/propriedades-unidade-de-medida.component';

@Component({
  selector: 'app-gerenciador-unidades-de-medida',
  templateUrl: './gerenciador-unidades-de-medida.component.html',
  styleUrls: ['./gerenciador-unidades-de-medida.component.scss']
})
export class GerenciadorUnidadesDeMedidaComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  unidadesDeMedida: UnidadeDeMedidaODT[];

  public colunas = [
    { field: 'codUnidadeDeMedida', header: 'Código' },
    { field: 'nome', header: 'Nome' },
    { field: 'unidade', header: 'Unidade' }
  ];

  constructor(
    public componentFactoryResolver: ComponentFactoryResolver,
    private unidadesDeMedidaService: UnidadesDeMedidaService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(PropriedadesUnidadeDeMedidaComponent);
  }

  private pesquisar(): void {
    this.unidadesDeMedidaService.Listar().subscribe((response: UnidadeDeMedidaODT[]) => {
      this.unidadesDeMedida = response;
    });
  }

  getModel = (model: UnidadeDeMedidaODT) => {
    return this.unidadesDeMedidaService.ObterPorId(model.codUnidadeDeMedida);
  }

  excluirModel = (model: UnidadeDeMedidaODT) => {
    return this.unidadesDeMedidaService.Excluir(model.codUnidadeDeMedida);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
