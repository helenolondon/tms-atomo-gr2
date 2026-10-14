import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import { AtmGerenciadorComponent } from 'src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { ProdutoODT } from '../odts/produto-odt';
import { ProdutosService } from '../services/produtos.service';
import { PropriedadesProdutoComponent } from '../propriedades-produto/propriedades-produto.component';

@Component({
  selector: 'app-gerenciador-produtos',
  templateUrl: './gerenciador-produtos.component.html',
  styleUrls: ['./gerenciador-produtos.component.scss']
})
export class GerenciadorProdutosComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  produtos: ProdutoODT[];

  public colunas = [
    { field: 'codProduto', header: 'Código', type: 'integer' },
    { field: 'descricao', header: 'Descrição' },
    { field: 'aka', header: 'Aka' },
    { field: 'precoDeCompra', header: 'Preço de compra', type: 'number' },
    { field: 'precoMedio', header: 'Preço médio', type: 'number' },
    { field: 'precoDeVenda', header: 'Preço de venda', type: 'number' },
    { field: 'codUnidadeDeMedida', header: 'Unidade', type: 'integer' },
    { field: 'codProdutoTipo', header: 'Tipo', type: 'integer' },
    { field: 'codMarca', header: 'Marca', type: 'integer' },
    { field: 'codProdutoGrupo', header: 'Grupo', type: 'integer' }
  ];

  constructor(
    public componentFactoryResolver: ComponentFactoryResolver,
    private produtosService: ProdutosService,
    private dialogsService: DialogsService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(PropriedadesProdutoComponent);
  }

  private pesquisar(): void {
    this.produtosService.ListarProdutos().subscribe((response: ProdutoODT[]) => {
      this.produtos = response;
    }, (error: HttpError) => {
      if (!(error instanceof HttpError) || !error.getErroTratado()) {
        this.dialogsService.erro('Erro ao carregar produtos');
      }
    });
  }

  getModel = (model: ProdutoODT) => {
    return this.produtosService.ObterProduto(model.codProduto);
  }

  excluirModel = (model: ProdutoODT) => {
    return this.produtosService.ExcluirProduto(model.codProduto);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
