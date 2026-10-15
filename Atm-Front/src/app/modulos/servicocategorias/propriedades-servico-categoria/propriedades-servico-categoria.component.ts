import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';
import { IAtmPropriedadesComponent } from '../../../infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { SalvarServicoCategoriaRequest } from '../requests/salvar-servico-categoria-request';
import { ServicoCategoriaODT } from '../odts/servico-categoria-odt';
import { ServicoCategoriasService } from '../services/servico-categorias.service';

@Component({
  selector: 'app-propriedades-servico-categoria',
  templateUrl: './propriedades-servico-categoria.component.html',
  styleUrls: ['./propriedades-servico-categoria.component.scss']
})
export class PropriedadesServicoCategoriaComponent implements IAtmPropriedadesComponent {
  servicoCategoriaForm: FormGroup = this.criarServicoCategoriaForm();
  serverErros: any = null;
  private originalForm: FormGroup;

  constructor(
    private servicoCategoriasService: ServicoCategoriasService,
    private dialogsService: DialogsService) { }

  setModel(model: ServicoCategoriaODT): void {
    this.servicoCategoriaForm.patchValue(model);
    this.originalForm = deepCloneObject(this.servicoCategoriaForm);
  }

  private criarServicoCategoriaForm(): FormGroup {
    const form = new FormGroup({
      codServicoCategoria: new FormControl({ value: 0, disabled: true }, [Validators.required]),
      nome: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      descricao: new FormControl('', [Validators.required, Validators.maxLength(250)])
    });
    this.originalForm = deepCloneObject(form);
    return form;
  }

  private viewToModel(): SalvarServicoCategoriaRequest {
    return {
      codServicoCategoria: this.servicoCategoriaForm.get('codServicoCategoria').value,
      nome: this.servicoCategoriaForm.get('nome').value,
      descricao: this.servicoCategoriaForm.get('descricao').value
    };
  }

  private salvar(model: SalvarServicoCategoriaRequest): void {
    this.servicoCategoriasService.SalvarServicoCategoria(model).subscribe((response: ServicoCategoriaODT) => {
      this.serverErros = null;
      this.dialogsService.sucesso('Categoria de serviço salva com sucesso');
      this.setModel(response);
    }, (error: HttpError) => {
      if (error instanceof HttpError && error.httpErrorResponse.status === 400) {
        this.serverErros = error.httpErrorResponse.error;
      } else if (!(error instanceof HttpError) || !error.getErroTratado()) {
        this.dialogsService.erro('Erro ao tentar salvar categoria de serviço');
      }
    });
  }

  onSubmit(): void {
    if (this.servicoCategoriaForm.invalid) {
      this.servicoCategoriaForm.markAsTouched();
      return;
    }

    this.salvar(this.viewToModel());
  }

  onResetForm(): void {
    this.servicoCategoriaForm = deepCloneObject(this.originalForm);
    this.serverErros = null;
  }
}
