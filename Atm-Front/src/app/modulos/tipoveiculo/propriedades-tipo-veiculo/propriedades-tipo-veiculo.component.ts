import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { TipoVeiculoODT } from '../odts/tipo-veiculo-odt';
import { SalvarTipoVeiculoRequest } from '../requests/salvar-tipo-veiculo-request';
import { TiposVeiculoService } from '../services/tipos-veiculo.service';

@Component({
  selector: 'app-propriedades-tipo-veiculo',
  templateUrl: './propriedades-tipo-veiculo.component.html',
  styleUrls: ['./propriedades-tipo-veiculo.component.scss']
})
export class PropriedadesTipoVeiculoComponent implements IAtmPropriedadesComponent {
  tipoVeiculoForm: FormGroup = this.criarTipoVeiculoForm();
  serverErros: any = null;
  private originalForm: FormGroup;

  constructor(
    private tiposVeiculoService: TiposVeiculoService,
    private dialogsService: DialogsService) { }

  setModel(model: TipoVeiculoODT): void {
    this.tipoVeiculoForm.patchValue(model);
    this.originalForm = deepCloneObject(this.tipoVeiculoForm);
  }

  private criarTipoVeiculoForm(): FormGroup {
    const form = new FormGroup({
      codTipoVeiculo: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      descricao: new FormControl('', [Validators.required, Validators.maxLength(250)])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private viewToModel(): SalvarTipoVeiculoRequest {
    return {
      codTipoVeiculo: this.tipoVeiculoForm.get('codTipoVeiculo').value,
      descricao: this.tipoVeiculoForm.get('descricao').value
    };
  }

  private salvar(model: SalvarTipoVeiculoRequest): void {
    this.tiposVeiculoService.Salvar(model).subscribe((response: TipoVeiculoODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Tipo de veículo salvo com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar tipo de veículo');
      }
    });
  }

  onSubmit(): void {
    if (this.tipoVeiculoForm.invalid) {
      this.tipoVeiculoForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.tipoVeiculoForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
