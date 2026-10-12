import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AtmComponentComponent } from 'src/app/infra/atom/components/atm-component/atm-component.component';
import { AtmButtonComponent } from 'src/app/infra/atom/components/controles/atm-button.component';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { RegistrarUsuarioRequest } from '../resquests/RegistrarUsuarioRequest';
import { UsuariosService } from '../services/usuarios.service';

@Component({
  selector: 'app-registrar-se',
  templateUrl: './registrar-se.component.html',
  styleUrls: ['./registrar-se.component.scss']
})
export class RegistrarSeComponent {

  @ViewChild(AtmComponentComponent, { static: false }) layoutComponent: AtmComponentComponent;
  @ViewChild("btnSalvar", { static: false }) btnSalvar: AtmButtonComponent;

  erros = [];

  usuariosForm = this.criarFormGroup();

  constructor(private fb: FormBuilder, private usuariosService: UsuariosService,
    private dialogs: DialogsService, private router: Router) { }

  get advogado(): boolean{
    return this.usuariosForm.get('advogado').value;
  }

  criarFormGroup() {
    let form =
      new FormGroup(
        {
          codigo: new FormControl(0, [Validators.required]),
          nome: new FormControl('', [Validators.required]),
          email: new FormControl('', [Validators.email, Validators.required]),
          cpfCnpj: new FormControl('', [Validators.required]),
          advogado: new FormControl(true, [Validators.required]),
          telefone: new FormControl('', [Validators.required]),
          numoab: new FormControl(null, [Validators.required])
        }
      );
    
      form.get('advogado').valueChanges.subscribe(valor => {
        
        let control = this.usuariosForm.get('numoab');

        control.clearValidators();

        if(valor){
          control.setValidators([Validators.required]);
        };

        control.updateValueAndValidity();        
      });

    return form;
  }

  onSubmit() {

    if (!this.usuariosForm.valid) {
      this.usuariosForm.markAsTouched();
      return;
    }

    this.onPostUsuario();
  }

  onPostUsuario() {

    this.erros = [];
    this.btnSalvar.aguarde("Salvando...");

    let
      request = new RegistrarUsuarioRequest(
        this.usuariosForm.get("nome").value,
        this.usuariosForm.get("cpfCnpj").value,
        this.usuariosForm.get("telefone").value,
        this.usuariosForm.get("email").value,
        this.usuariosForm.get("advogado").value,
        this.usuariosForm.get("numoab").value
      );
    
    console.log(request);

    this.usuariosService.RegistrarUsuario(request).subscribe((response) => {
      this.btnSalvar.aguarde(null);
      this.dialogs.showModalMessage("Login e senha enviados por e-mail.", () => { this.router.navigate(['/login']); }, 'Registro concluído!');
    },
      (err: HttpError) => {
        this.btnSalvar.aguarde(null);

        if (!err.getErroTratado()) {
          this.erros.push(err.httpErrorResponse.error);
        };
      });
  };
}
