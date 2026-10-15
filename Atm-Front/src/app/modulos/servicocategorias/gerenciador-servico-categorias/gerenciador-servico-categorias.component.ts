import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import { AtmGerenciadorComponent } from 'src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { PropriedadesServicoCategoriaComponent } from '../propriedades-servico-categoria/propriedades-servico-categoria.component';
import { ServicoCategoriaODT } from '../odts/servico-categoria-odt';
import { ServicoCategoriasService } from '../services/servico-categorias.service';

@Component({
  selector: 'app-gerenciador-servico-categorias',
  templateUrl: './gerenciador-servico-categorias.component.html',
  styleUrls: ['./gerenciador-servico-categorias.component.scss']
})
export class GerenciadorServicoCategoriasComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  servicoCategorias: ServicoCategoriaODT[];

  public colunas = [
    { field: 'codServicoCategoria', header: 'Código', type: 'integer' },
    { field: 'nome', header: 'Nome' },
    { field: 'descricao', header: 'Descrição' }
  ];

  constructor(
    public componentFactoryResolver: ComponentFactoryResolver,
    private servicoCategoriasService: ServicoCategoriasService,
    private dialogsService: DialogsService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(PropriedadesServicoCategoriaComponent);
  }

  private pesquisar(): void {
    this.servicoCategoriasService.ListarServicoCategorias().subscribe((response: ServicoCategoriaODT[]) => {
      this.servicoCategorias = response;
    }, (error: HttpError) => {
      if (!(error instanceof HttpError) || !error.getErroTratado()) {
        this.dialogsService.erro('Erro ao carregar categorias de serviço');
      }
    });
  }

  getModel = (model: ServicoCategoriaODT) => {
    return this.servicoCategoriasService.ObterServicoCategoria(model.codServicoCategoria);
  }

  excluirModel = (model: ServicoCategoriaODT) => {
    return this.servicoCategoriasService.ExcluirServicoCategoria(model.codServicoCategoria);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
