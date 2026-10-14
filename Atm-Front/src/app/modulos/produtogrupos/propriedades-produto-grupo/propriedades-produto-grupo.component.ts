import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { ProdutoGrupoODT } from '../odts/produto-grupo-odt';
import { SalvarProdutoGrupoRequest } from '../requests/salvar-produto-grupo-request';
import { ProdutoGruposService } from '../services/produto-grupos.service';

@Component({
  selector: 'app-propriedades-produto-grupo',
  templateUrl: './propriedades-produto-grupo.component.html',
  styleUrls: ['./propriedades-produto-grupo.component.scss']
})
export class PropriedadesProdutoGrupoComponent implements IAtmPropriedadesComponent {
  produtoGrupoForm: FormGroup = this.criarProdutoGrupoForm();
  serverErros: any = null;
  private originalForm: FormGroup;

  constructor(private produtoGruposService: ProdutoGruposService, private dialogsService: DialogsService) { }

  setModel(model: ProdutoGrupoODT): void {
    this.produtoGrupoForm.patchValue(model);
    this.originalForm = deepCloneObject(this.produtoGrupoForm);
  }

  private criarProdutoGrupoForm(): FormGroup {
    const form = new FormGroup({
      codProdutoGrupo: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      nome: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      descricao: new FormControl('', [Validators.required, Validators.maxLength(250)])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private viewToModel(): SalvarProdutoGrupoRequest {
    return {
      codProdutoGrupo: this.produtoGrupoForm.get('codProdutoGrupo').value,
      nome: this.produtoGrupoForm.get('nome').value,
      descricao: this.produtoGrupoForm.get('descricao').value
    };
  }

  private salvar(model: SalvarProdutoGrupoRequest): void {
    this.produtoGruposService.SalvarProdutoGrupo(model).subscribe((response: ProdutoGrupoODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Grupo de produto salvo com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error instanceof HttpError && error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!(error instanceof HttpError) || !error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar grupo de produto');
      }
    });
  }

  onSubmit(): void {
    if (this.produtoGrupoForm.invalid) {
      this.produtoGrupoForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.produtoGrupoForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
