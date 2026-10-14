import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import { AtmGerenciadorComponent } from 'src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { ProdutoGrupoODT } from '../odts/produto-grupo-odt';
import { PropriedadesProdutoGrupoComponent } from '../propriedades-produto-grupo/propriedades-produto-grupo.component';
import { ProdutoGruposService } from '../services/produto-grupos.service';

@Component({
  selector: 'app-gerenciador-produto-grupos',
  templateUrl: './gerenciador-produto-grupos.component.html',
  styleUrls: ['./gerenciador-produto-grupos.component.scss']
})
export class GerenciadorProdutoGruposComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  produtoGrupos: ProdutoGrupoODT[];

  public colunas = [
    { field: 'codProdutoGrupo', header: 'Código', type: 'integer' },
    { field: 'nome', header: 'Nome' },
    { field: 'descricao', header: 'Descrição' }
  ];

  constructor(
    public componentFactoryResolver: ComponentFactoryResolver,
    private produtoGruposService: ProdutoGruposService,
    private dialogsService: DialogsService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(PropriedadesProdutoGrupoComponent);
  }

  private pesquisar(): void {
    this.produtoGruposService.ListarProdutoGrupos().subscribe((response: ProdutoGrupoODT[]) => {
      this.produtoGrupos = response;
    }, (error: HttpError) => {
      if (!(error instanceof HttpError) || !error.getErroTratado()) {
        this.dialogsService.erro('Erro ao carregar grupos de produto');
      }
    });
  }

  getModel = (model: ProdutoGrupoODT) => {
    return this.produtoGruposService.ObterProdutoGrupo(model.codProdutoGrupo);
  }

  excluirModel = (model: ProdutoGrupoODT) => {
    return this.produtoGruposService.ExcluirProdutoGrupo(model.codProdutoGrupo);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
