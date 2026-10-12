import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { LoginModel } from '../models/Login.model';
import { DxFormComponent } from 'devextreme-angular';
import { AutenticaService } from 'src/app/modulos/autenticacao/services/autentica.service';
import { Router } from '@angular/router';
import { HttpError } from 'src/app/infra/services/models/HttpError';
import { AtomAlertComponent } from 'src/app/infra/atom/components/mensagens/atom-alert/atom-alert.component';
import { AtmComponentComponent } from 'src/app/infra/atom/components/atm-component/atm-component.component';
import { PersistenceService } from 'src/app/infra/services/persistence.service';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  @ViewChild(DxFormComponent, { static: false }) loginForm: DxFormComponent;
  @ViewChild("loginAlert", {static: false}) loginAlert: AtomAlertComponent;
  @ViewChild(AtmComponentComponent, {static: false} ) layoutComponent: AtmComponentComponent;

  public login: LoginModel;
  public efetuandoLogin: boolean;
  public permanecerLogado;

  private perLogadoSKey = "atmLogadoSKey";

  buttonOptions: any = {
    text: "Enviar",
    type: "default",
    width: "100%",
    useSubmitBehavior: true
  }

  constructor(private autentica: AutenticaService, private router: Router, private persistenceService: PersistenceService ) {
    this.login = new LoginModel();
    this.efetuandoLogin = false;
    this.permanecerLogado = persistenceService.getItemAsBoolean(this.perLogadoSKey);
  }

  loginSubmit(e) {

    let val = this.loginForm.instance.validate();

    e.preventDefault();

    if (!val.isValid) {
      return;
    }

    if(this.efetuandoLogin){
      return;
    }

    this.efetuandoLogin = true;

    this.autentica.Autenticar(this.login.login, this.login.senha, this.permanecerLogado)
    .subscribe(
      login => {
        if(login !== null){
          this.router.navigate(["/"]);
        }

        this.persistenceService.setItemAsBoolean(this.perLogadoSKey, this.permanecerLogado);
        this.efetuandoLogin = false;
      },
      (err: HttpError) => {
        if(err.naoAutorizado){
          this.loginAlert.hidden = false;
        }
        else{
          console.log(err);

          if(!isNullOrUndefined(err.httpErrorResponse.status) && err.httpErrorResponse.status > 0 && !err.getErroTratado()){
            this.layoutComponent.erros.push(err.httpErrorResponse.statusText);          
          }
        };

        this.efetuandoLogin = false;
      }
    )    
  }

  resetLoginForm(e){
    this.loginForm.instance.resetValues();
    this.loginAlert.hidden = true;
  }
}
