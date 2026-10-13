import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { UnidadeDeMedidaODT } from '../odts/unidade-de-medida-odt';
import { SalvarUnidadeDeMedidaRequest } from '../requests/salvar-unidade-de-medida-request';
import { UnidadesDeMedidaService } from '../services/unidades-de-medida.service';

@Component({
  selector: 'app-propriedades-unidade-de-medida',
  templateUrl: './propriedades-unidade-de-medida.component.html',
  styleUrls: ['./propriedades-unidade-de-medida.component.scss']
})
export class PropriedadesUnidadeDeMedidaComponent implements IAtmPropriedadesComponent {
  unidadeDeMedidaForm: FormGroup = this.criarUnidadeDeMedidaForm();
  serverErros: any = null;
  private originalForm: FormGroup;

  constructor(
    private unidadesDeMedidaService: UnidadesDeMedidaService,
    private dialogsService: DialogsService) { }

  setModel(model: UnidadeDeMedidaODT): void {
    this.unidadeDeMedidaForm.patchValue(model);
    this.originalForm = deepCloneObject(this.unidadeDeMedidaForm);
  }

  private criarUnidadeDeMedidaForm(): FormGroup {
    const form = new FormGroup({
      codUnidadeDeMedida: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      nome: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      unidade: new FormControl('', [Validators.required, Validators.maxLength(3)])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private viewToModel(): SalvarUnidadeDeMedidaRequest {
    return {
      codUnidadeDeMedida: this.unidadeDeMedidaForm.get('codUnidadeDeMedida').value,
      nome: this.unidadeDeMedidaForm.get('nome').value,
      unidade: this.unidadeDeMedidaForm.get('unidade').value
    };
  }

  private salvar(model: SalvarUnidadeDeMedidaRequest): void {
    this.unidadesDeMedidaService.Salvar(model).subscribe((response: UnidadeDeMedidaODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Unidade de medida salva com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar unidade de medida');
      }
    });
  }

  onSubmit(): void {
    if (this.unidadeDeMedidaForm.invalid) {
      this.unidadeDeMedidaForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.unidadeDeMedidaForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
