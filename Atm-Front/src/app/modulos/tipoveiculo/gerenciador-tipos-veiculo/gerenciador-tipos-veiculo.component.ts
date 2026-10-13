import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import {
  AtmGerenciadorComponent
} from '../../../infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { TipoVeiculoODT } from '../odts/tipo-veiculo-odt';
import { TiposVeiculoService } from '../services/tipos-veiculo.service';
import {
  PropriedadesTipoVeiculoComponent
} from '../propriedades-tipo-veiculo/propriedades-tipo-veiculo.component';

@Component({
  selector: 'app-gerenciador-tipos-veiculo',
  templateUrl: './gerenciador-tipos-veiculo.component.html',
  styleUrls: ['./gerenciador-tipos-veiculo.component.scss']
})
export class GerenciadorTiposVeiculoComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  tiposVeiculo: TipoVeiculoODT[];

  public colunas = [
    { field: 'codTipoVeiculo', header: 'Código' },
    { field: 'descricao', header: 'Descrição' }
  ];

  constructor(
    public componentFactoryResolver: ComponentFactoryResolver,
    private tiposVeiculoService: TiposVeiculoService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(PropriedadesTipoVeiculoComponent);
  }

  private pesquisar(): void {
    this.tiposVeiculoService.Listar().subscribe((response: TipoVeiculoODT[]) => {
      this.tiposVeiculo = response;
    });
  }

  getModel = (model: TipoVeiculoODT) => {
    return this.tiposVeiculoService.ObterPorId(model.codTipoVeiculo);
  }

  excluirModel = (model: TipoVeiculoODT) => {
    return this.tiposVeiculoService.Excluir(model.codTipoVeiculo);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
