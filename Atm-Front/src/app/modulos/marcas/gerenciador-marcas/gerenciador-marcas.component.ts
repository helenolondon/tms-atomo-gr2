import { AfterViewInit, Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import { AtmGerenciadorComponent } from 'src/app/infra/atom/components/atm-gerenciador/atm-gerenciador.component';
import { MarcaODT } from '../odts/marca-odt';
import { MarcasService } from '../services/marcas.service';
import { PropriedadesMarcaComponent } from '../propriedades-marca/propriedades-marca.component';

@Component({
  selector: 'app-gerenciador-marcas',
  templateUrl: './gerenciador-marcas.component.html',
  styleUrls: ['./gerenciador-marcas.component.scss']
})
export class GerenciadorMarcasComponent implements AfterViewInit {
  @ViewChild(AtmGerenciadorComponent, { static: true }) atmGerenciador: AtmGerenciadorComponent | undefined;

  marcas: MarcaODT[];

  public colunas = [
    { field: 'codMarca', header: 'Código' },
    { field: 'nome', header: 'Nome' },
    { field: 'descricao', header: 'Descrição' },
    { field: 'observacao', header: 'Observação' }
  ];

  constructor(public componentFactoryResolver: ComponentFactoryResolver, private marcasService: MarcasService) {
    this.pesquisar();
  }

  ngAfterViewInit(): void {
    this.atmGerenciador.componentFactory = this.componentFactoryResolver.resolveComponentFactory(PropriedadesMarcaComponent);
  }

  private pesquisar(): void {
    this.marcasService.ListarMarcas().subscribe((response: MarcaODT[]) => {
      this.marcas = response;
    });
  }

  getModel = (model: MarcaODT) => {
    return this.marcasService.ObterMarca(model.codMarca);
  }

  excluirModel = (model: MarcaODT) => {
    return this.marcasService.ExcluirMarca(model.codMarca);
  }

  onPesquisaSubmit(): void {
    this.pesquisar();
  }
}
