import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { MarcaODT } from '../../marcas/odts/marca-odt';
import { MarcasService } from '../../marcas/services/marcas.service';
import { ProdutoTipoODT } from '../../produtotipos/odts/produto-tipo-odt';
import { ProdutoTiposService } from '../../produtotipos/services/produto-tipos.service';
import { UnidadeDeMedidaODT } from '../../unidadesdemedida/odts/unidade-de-medida-odt';
import { UnidadesDeMedidaService } from '../../unidadesdemedida/services/unidades-de-medida.service';
import { ProdutoODT } from '../odts/produto-odt';
import { SalvarProdutoRequest } from '../requests/salvar-produto-request';
import { ProdutosService } from '../services/produtos.service';
import { ProdutoGrupoODT } from '../../produtogrupos/odts/produto-grupo-odt';
import { ProdutoGruposService } from '../../produtogrupos/services/produto-grupos.service';

@Component({
  selector: 'app-propriedades-produto',
  templateUrl: './propriedades-produto.component.html',
  styleUrls: ['./propriedades-produto.component.scss']
})
export class PropriedadesProdutoComponent implements IAtmPropriedadesComponent {
  produtoForm: FormGroup = this.criarProdutoForm();
  serverErros: any = null;
  unidadesDeMedida = [];
  tiposDeProduto = [];
  marcas = [];
  gruposDeProduto = [];
  private originalForm: FormGroup;

  constructor(
    private produtosService: ProdutosService,
    private produtoGruposService: ProdutoGruposService,
    private produtoTiposService: ProdutoTiposService,
    private unidadesDeMedidaService: UnidadesDeMedidaService,
    private marcasService: MarcasService,
    private dialogsService: DialogsService) {
    this.carregarLookups();
  }

  setModel(model: ProdutoODT): void {
    this.produtoForm.patchValue(model);
    this.originalForm = deepCloneObject(this.produtoForm);
  }

  private criarProdutoForm(): FormGroup {
    const form = new FormGroup({
      codProduto: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      descricao: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      aka: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      precoDeCompra: new FormControl(null),
      precoMedio: new FormControl(null),
      precoDeVenda: new FormControl(null),
      codUnidadeDeMedida: new FormControl(null, [Validators.required]),
      codProdutoTipo: new FormControl(null, [Validators.required]),
      codMarca: new FormControl(null, [Validators.required]),
      codProdutoGrupo: new FormControl(null, [Validators.required])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private carregarLookups(): void {
    forkJoin([
      this.unidadesDeMedidaService.Listar(),
      this.produtoTiposService.ListarProdutoTipos(),
      this.marcasService.ListarMarcas(),
      this.produtoGruposService.ListarProdutoGrupos()
    ]).subscribe((response: [UnidadeDeMedidaODT[], ProdutoTipoODT[], MarcaODT[], ProdutoGrupoODT[]]) => {
      this.unidadesDeMedida = this.toLookupItems(
        response[0], 'codUnidadeDeMedida', (item: UnidadeDeMedidaODT) => item.nome + ' (' + item.unidade + ')');
      this.tiposDeProduto = this.toLookupItems(
        response[1], 'codProdutoTipo', (item: ProdutoTipoODT) => item.nome);
      this.marcas = this.toLookupItems(
        response[2], 'codMarca', (item: MarcaODT) => item.nome);
      this.gruposDeProduto = this.toLookupItems(
        response[3], 'codProdutoGrupo', (item: ProdutoGrupoODT) => item.nome);
    }, (error: HttpError) => {
      if (!(error instanceof HttpError) || !error.getErroTratado()) {
        this.dialogsService.erro('Erro ao carregar as opções de produto');
      }
    });
  }

  private toLookupItems<T>(items: T[], idField: string, label: (item: T) => string): any[] {
    return [{ descricao: '-- Selecione --', codigo: null }].concat(items.map((item: T) => ({
      descricao: label(item),
      codigo: item[idField]
    })));
  }

  private viewToModel(): SalvarProdutoRequest {
    return {
      codProduto: this.produtoForm.get('codProduto').value,
      descricao: this.produtoForm.get('descricao').value,
      aka: this.produtoForm.get('aka').value,
      precoDeCompra: this.toOptionalNumber('precoDeCompra'),
      precoMedio: this.toOptionalNumber('precoMedio'),
      precoDeVenda: this.toOptionalNumber('precoDeVenda'),
      codUnidadeDeMedida: this.produtoForm.get('codUnidadeDeMedida').value,
      codProdutoTipo: this.produtoForm.get('codProdutoTipo').value,
      codMarca: this.produtoForm.get('codMarca').value,
      codProdutoGrupo: this.produtoForm.get('codProdutoGrupo').value
    };
  }

  private toOptionalNumber(controlName: string): number {
    const value = this.produtoForm.get(controlName).value;
    return value === '' || value === null ? null : Number(value);
  }

  private salvar(model: SalvarProdutoRequest): void {
    this.produtosService.SalvarProduto(model).subscribe((response: ProdutoODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Produto salvo com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error instanceof HttpError && error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!(error instanceof HttpError) || !error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar produto');
      }
    });
  }

  onSubmit(): void {
    if (this.produtoForm.invalid) {
      this.produtoForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.produtoForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
