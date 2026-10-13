import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { MarcaODT } from '../odts/marca-odt';
import { SalvarMarcaRequest } from '../requests/salvar-marca-request';
import { MarcasService } from '../services/marcas.service';

@Component({
  selector: 'app-propriedades-marca',
  templateUrl: './propriedades-marca.component.html',
  styleUrls: ['./propriedades-marca.component.scss']
})
export class PropriedadesMarcaComponent implements IAtmPropriedadesComponent {
  marcaForm: FormGroup = this.criarMarcaForm();
  serverErros: any = null;
  private originalForm: FormGroup;

  constructor(private marcasService: MarcasService, private dialogsService: DialogsService) { }

  setModel(model: MarcaODT): void {
    this.marcaForm.patchValue(model);
    this.originalForm = deepCloneObject(this.marcaForm);
  }

  private criarMarcaForm(): FormGroup {
    const form = new FormGroup({
      codMarca: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      nome: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      descricao: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      observacao: new FormControl('', [Validators.maxLength(500)])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private viewToModel(): SalvarMarcaRequest {
    return {
      codMarca: this.marcaForm.get('codMarca').value,
      nome: this.marcaForm.get('nome').value,
      descricao: this.marcaForm.get('descricao').value,
      observacao: this.marcaForm.get('observacao').value || ''
    };
  }

  private salvar(model: SalvarMarcaRequest): void {
    this.marcasService.SalvarMarca(model).subscribe((response: MarcaODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Marca salva com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar marca');
      }
    });
  }

  onSubmit(): void {
    if (this.marcaForm.invalid) {
      this.marcaForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.marcaForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
