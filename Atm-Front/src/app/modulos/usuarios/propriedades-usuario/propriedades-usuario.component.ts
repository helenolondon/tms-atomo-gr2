import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, NgControl, Validators } from '@angular/forms';
import validator from 'devextreme/ui/validator';
import { EventEmitter } from 'selenium-webdriver';
import { AtmComponentComponent } from 'src/app/infra/atom/components/atm-component/atm-component.component';
import { IAtmPropriedadesComponent } from 'src/app/infra/atom/components/atm-propriedades/IAtm-Propriedades-Component';
import { AtmButtonComponent } from 'src/app/infra/atom/components/controles/atm-button.component';
import { DialogsService } from 'src/app/infra/services/dialogs.service';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { AlterarSenhaRequest } from '../../autenticacao/requests/alterar-senha';
import { SalvarUsuarioRequest } from '../resquests/SalvarUsuarioRequest';
import { UsuariosService } from '../services/usuarios.service';

@Component({
  selector: 'atm-propriedades-usuario',
  templateUrl: './propriedades-usuario.component.html',
  styleUrls: ['./propriedades-usuario.component.scss']
})
export class PropriedadesUsuarioComponent implements IAtmPropriedadesComponent {

  @ViewChild(AtmComponentComponent, {static: false} ) layoutComponent: AtmComponentComponent;
  @ViewChild("btnSalvar", {static: false}) btnSalvar: AtmButtonComponent;
  
  erros = [];

  usuariosForm = new FormGroup(
    {
      codigo: new FormControl(0, [Validators.required]),
      nome: new FormControl('', [Validators.required]),
      senha: new FormControl('', [Validators.required]),
      confirmaSenha: new FormControl('', [Validators.required]),
      login: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.email]),
      telefone: new FormControl(),
      dataCadastro: new FormControl({value: new Date(), disabled: true}),
      lembrarSenha:  new FormControl(false)
    }
  );

  constructor(private fb: FormBuilder, private usuariosService: UsuariosService,
    private dialogs: DialogsService) { }

  onSubmit(){

    if(!this.usuariosForm.valid){
      this.usuariosForm.markAsTouched();
      return;
    }

    this.onPostUsuario();
  }

  onPostUsuario(){

    this.erros = [];

    if(this.usuariosForm.get("confirmaSenha").value !== this.usuariosForm.get("senha").value){
      this.dialogs.erro("Senhas não conferem");
      return;
    }

    this.btnSalvar.aguarde("Salvando...");

    let
      request = new SalvarUsuarioRequest(
      this.usuariosForm.get("codigo").value, 
      this.usuariosForm.get("nome").value,
      this.usuariosForm.get("email").value, 
      this.usuariosForm.get("login").value,
      this.usuariosForm.get("telefone").value,
      this.usuariosForm.get("senha").value
    );

    this.usuariosService.SalvaUsuario(request).subscribe((response) => {
      this.btnSalvar.aguarde(null);
      this.dialogs.sucesso("Usuário salvo com sucesso");

      this.setModel(response);
    },
    (err: HttpError) =>{
      this.btnSalvar.aguarde(null);

      if(!err.getErroTratado()){
        this.erros.push(err.httpErrorResponse.error);
      };
    });
  };

  setModel(model: any){
    try{
      
      this.usuariosForm.setValue({
        'codigo': model.id,
        'dataCadastro': new Date(model.dataDeCadastro),
        'nome': model.nome,
        'email': model.email,
        'login': model.login,
        'telefone': model.telefone,
        'senha': '???????????????????????',
        'confirmaSenha': '???????????????????????',
        'lembrarSenha': null
      });

      if(model.id > 0){
        this.usuariosForm.controls['senha'].disable();
        this.usuariosForm.controls['confirmaSenha'].disable();
        this.usuariosForm.controls['login'].disable();
      };

    } catch (e) {
      console.log(e);
      throw "Erro ao carregar dados do usuário";      
    }
  };
}
