import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../../../infra/atom/validators/password-match-validator';
import { DialogsService } from '../../../infra/services/dialogs.service';
import { HttpError } from '../../../infra/services/models/HttpError';
import { UsuariosService } from '../services/usuarios.service';

@Component({
  selector: 'app-alterar-senha-usuario',
  templateUrl: './alterar-senha-usuario.component.html',
  styleUrls: ['./alterar-senha-usuario.component.scss']
})
export class AlterarSenhaUsuarioComponent {

  alterarSenhaFG = new FormGroup({
    login: new FormControl('', [Validators.required]),
    senha: new FormControl('', [Validators.required]),
    novaSenha: new FormControl('', [Validators.required]),
    confirmaSenha: new FormControl('', [Validators.required])
  })

  senhaConfirmada: boolean = true;
  
  constructor(private service: UsuariosService, private dialogs: DialogsService) { }

  onSubmit(){
    this.senhaConfirmada = this.alterarSenhaFG.controls['novaSenha'].value == this.alterarSenhaFG.controls['confirmaSenha'].value;

    if(!this.alterarSenhaFG.valid){
      this.alterarSenhaFG.markAsTouched();
      return;
    };


    this.onPostAlterarSenha();
  }

  onPostAlterarSenha(){
    // debugger;
    this.service.AlterarSenha(this.alterarSenhaFG.controls['login'].value, this.alterarSenhaFG.controls['senha'].value,
    this.alterarSenhaFG.controls['novaSenha'].value, this.alterarSenhaFG.controls['confirmaSenha'].value)
    .subscribe( (resp) => {
      this.dialogs.sucesso(resp);
    }, 
    (err: HttpError) => {
      debugger;
      if(!err.getErroTratado()){
        this.dialogs.erro(err.httpErrorResponse.error);
      };
    });
  }
}
