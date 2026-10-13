import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { ProdutoTipoAgressividadeODT } from '../odts/produto-tipo-agressividade-odt';
import { SalvarProdutoTipoAgressividadeRequest } from '../requests/salvar-produto-tipo-agressividade-request';
import { ProdutoTipoAgressividadesService } from '../services/produto-tipo-agressividades.service';

@Component({
  selector: 'app-propriedades-produto-tipo-agressividade',
  templateUrl: './propriedades-produto-tipo-agressividade.component.html',
  styleUrls: ['./propriedades-produto-tipo-agressividade.component.scss']
})
export class PropriedadesProdutoTipoAgressividadeComponent implements IAtmPropriedadesComponent {
  produtoTipoAgressividadeForm: FormGroup = this.criarProdutoTipoAgressividadeForm();
  serverErros: any = null;
  private originalForm: FormGroup;

  constructor(
    private produtoTipoAgressividadesService: ProdutoTipoAgressividadesService,
    private dialogsService: DialogsService) { }

  setModel(model: ProdutoTipoAgressividadeODT): void {
    this.produtoTipoAgressividadeForm.patchValue(model);
    this.originalForm = deepCloneObject(this.produtoTipoAgressividadeForm);
  }

  private criarProdutoTipoAgressividadeForm(): FormGroup {
    const form = new FormGroup({
      codProdutoTipoAgressividade: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      nome: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      descricao: new FormControl('', [Validators.maxLength(250)])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private viewToModel(): SalvarProdutoTipoAgressividadeRequest {
    return {
      codProdutoTipoAgressividade: this.produtoTipoAgressividadeForm.get('codProdutoTipoAgressividade').value,
      nome: this.produtoTipoAgressividadeForm.get('nome').value,
      descricao: this.produtoTipoAgressividadeForm.get('descricao').value || ''
    };
  }

  private salvar(model: SalvarProdutoTipoAgressividadeRequest): void {
    this.produtoTipoAgressividadesService.Salvar(model).subscribe((response: ProdutoTipoAgressividadeODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Tipo de agressividade salvo com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar tipo de agressividade');
      }
    });
  }

  onSubmit(): void {
    if (this.produtoTipoAgressividadeForm.invalid) {
      this.produtoTipoAgressividadeForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.produtoTipoAgressividadeForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
