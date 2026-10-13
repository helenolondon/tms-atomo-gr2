import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { ProdutoTipoODT } from '../odts/produto-tipo-odt';
import { SalvarProdutoTipoRequest } from '../requests/salvar-produto-tipo-request';
import { ProdutoTiposService } from '../services/produto-tipos.service';

@Component({
  selector: 'app-propriedades-produto-tipo',
  templateUrl: './propriedades-produto-tipo.component.html',
  styleUrls: ['./propriedades-produto-tipo.component.scss']
})
export class PropriedadesProdutoTipoComponent implements IAtmPropriedadesComponent {
  produtoTipoForm: FormGroup = this.criarProdutoTipoForm();
  serverErros: any = null;
  private originalForm: FormGroup;

  constructor(private produtoTiposService: ProdutoTiposService, private dialogsService: DialogsService) { }

  setModel(model: ProdutoTipoODT): void {
    this.produtoTipoForm.patchValue(model);
    this.originalForm = deepCloneObject(this.produtoTipoForm);
  }

  private criarProdutoTipoForm(): FormGroup {
    const form = new FormGroup({
      codProdutoTipo: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      nome: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      descricao: new FormControl('', [Validators.required, Validators.maxLength(250)])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private viewToModel(): SalvarProdutoTipoRequest {
    return {
      codProdutoTipo: this.produtoTipoForm.get('codProdutoTipo').value,
      nome: this.produtoTipoForm.get('nome').value,
      descricao: this.produtoTipoForm.get('descricao').value
    };
  }

  private salvar(model: SalvarProdutoTipoRequest): void {
    this.produtoTiposService.SalvarProdutoTipo(model).subscribe((response: ProdutoTipoODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Tipo de produto salvo com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar tipo de produto');
      }
    });
  }

  onSubmit(): void {
    if (this.produtoTipoForm.invalid) {
      this.produtoTipoForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.produtoTipoForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
