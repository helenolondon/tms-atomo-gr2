import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import {
  AtmGerenciadorComponent
} from '../../../infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { ProdutoTipoAgressividadeODT } from '../odts/produto-tipo-agressividade-odt';
import { ProdutoTipoAgressividadesService } from '../services/produto-tipo-agressividades.service';
import {
  PropriedadesProdutoTipoAgressividadeComponent
} from '../propriedades-produto-tipo-agressividade/propriedades-produto-tipo-agressividade.component';

@Component({
  selector: 'app-gerenciador-produto-tipo-agressividades',
  templateUrl: './gerenciador-produto-tipo-agressividades.component.html',
  styleUrls: ['./gerenciador-produto-tipo-agressividades.component.scss']
})
export class GerenciadorProdutoTipoAgressividadesComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  produtoTipoAgressividades: ProdutoTipoAgressividadeODT[];

  public colunas = [
    { field: 'codProdutoTipoAgressividade', header: 'Código' },
    { field: 'nome', header: 'Nome' },
    { field: 'descricao', header: 'Descrição' }
  ];

  constructor(
    public componentFactoryResolver: ComponentFactoryResolver,
    private produtoTipoAgressividadesService: ProdutoTipoAgressividadesService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(PropriedadesProdutoTipoAgressividadeComponent);
  }

  private pesquisar(): void {
    this.produtoTipoAgressividadesService.Listar().subscribe((response: ProdutoTipoAgressividadeODT[]) => {
      this.produtoTipoAgressividades = response;
    });
  }

  getModel = (model: ProdutoTipoAgressividadeODT) => {
    return this.produtoTipoAgressividadesService.ObterPorId(model.codProdutoTipoAgressividade);
  }

  excluirModel = (model: ProdutoTipoAgressividadeODT) => {
    return this.produtoTipoAgressividadesService.Excluir(model.codProdutoTipoAgressividade);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
