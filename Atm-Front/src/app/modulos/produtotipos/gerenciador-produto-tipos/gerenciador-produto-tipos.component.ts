import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import { AtmGerenciadorComponent } from 'src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { ProdutoTipoODT } from '../odts/produto-tipo-odt';
import { ProdutoTiposService } from '../services/produto-tipos.service';
import { PropriedadesProdutoTipoComponent } from '../propriedades-produto-tipo/propriedades-produto-tipo.component';

@Component({
  selector: 'app-gerenciador-produto-tipos',
  templateUrl: './gerenciador-produto-tipos.component.html',
  styleUrls: ['./gerenciador-produto-tipos.component.scss']
})
export class GerenciadorProdutoTiposComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  produtoTipos: ProdutoTipoODT[];

  public colunas = [
    { field: 'codProdutoTipo', header: 'Código' },
    { field: 'nome', header: 'Nome' },
    { field: 'descricao', header: 'Descrição' }
  ];

  constructor(public componentFactoryResolver: ComponentFactoryResolver, private produtoTiposService: ProdutoTiposService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(PropriedadesProdutoTipoComponent);
  }

  private pesquisar(): void {
    this.produtoTiposService.ListarProdutoTipos().subscribe((response: ProdutoTipoODT[]) => {
      this.produtoTipos = response;
    });
  }

  getModel = (model: ProdutoTipoODT) => {
    return this.produtoTiposService.ObterProdutoTipo(model.codProdutoTipo);
  }

  excluirModel = (model: ProdutoTipoODT) => {
    return this.produtoTiposService.ExcluirProdutoTipo(model.codProdutoTipo);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
