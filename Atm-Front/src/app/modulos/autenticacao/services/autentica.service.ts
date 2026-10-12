/**
 * Autor: Heleno Sales Mesquita
 * Data : 18/01/2020
 * 
 * Servicos de autenticação
 */
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { LoginModel } from 'src/app/site/models/Login.model';
import { map, catchError } from 'rxjs/operators';
import { LoginModelMapper } from '../mappers/LoginModel.Mapper';
import { Router } from '@angular/router';
import { PersistenceService } from 'src/app/infra/services/persistence.service';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { LoginResponse } from 'src/app/infra/services/responses/LoginResponse';
import { LoginRequest } from 'src/app/infra/services/requests/LoginRequest';
import { AlterarSenhaRequest } from '../requests/alterar-senha';
import { UsuarioODT } from '../../usuarios/odts/usuario-odt';
import { deepCloneObject } from 'src/app/infra/utils/js-utils';

@Injectable({
  providedIn: 'root'
})
export class AutenticaService {
  private _token;
  private loginStoreKey: string = "AtmLoginInfo";
  private usuarioLogado: UsuarioODT = null;
  
  get Autenticado(): boolean{
    if(this._token == null){
      this.loadLoginInfo();
    }
    
    return this.Token !== null;
  };

  get UsuarioLogado(): UsuarioODT{
    let obj = this.persistenceService.getItemObject(this.loginStoreKey) as LoginModel;
    
    return obj.usuario;
  }

  get Token(): string {
    return this._token;
  }

  private loadLoginInfo(){
    let obj = this.persistenceService.getItemObject(this.loginStoreKey) as LoginModel;

    if(obj && obj.token){
      this._token = obj.token;
    }
  }
   
 public Autenticar(login: string, senha: string, permanecer: Boolean): Observable<LoginResponse>{
  return this.httpClient
    .post<LoginResponse>(this.httpSettings.ServerUrl + "auth/Autenticacao/login", new LoginRequest(login, senha), 
    this.httpSettings.RetHttpRequestDefaultOptions())
    .pipe(
      map((response, index) => { 
        if(!response){
          throwError("Api de login não retornou uma reposta");
        }
        
        try {
          this._token = response.token;
          
          this.usuarioLogado = response.usuario;
          
          if(!this._token){
            throwError("Login não retornou um token válido");
          }
          
          if(response == null){
            throwError("Erro ao converter responsta de login");
          } 

          let loginSessionObj: LoginResponse = deepCloneObject(response);

          if(!permanecer){
            loginSessionObj.token = null;
          };

          this.persistenceService.setObject(this.loginStoreKey, loginSessionObj);  

          return response;
        } catch (error) {
          this._token = null;
          throwError("Erro ao tentar mapear a resposta do Login");
        }

        return response;
      }),
      catchError(e => { return this.httpSettings.HandleHttpError(e) } )
    );
  };

  public verifyToken(): Observable<boolean>{
    return this.httpClient.get<HttpResponse<any>>(this.httpSettings.ServerUrl + "auth/Autenticacao/verify")
    .pipe<boolean>(
      map(resp => {
        return resp.status === 200;
      })
    );
  }

  public LogOut(){
    this._token = null;
    this.persistenceService.removeItem(this.loginStoreKey);
    this.router.navigate(["/login"]);
  }

  public AlterarSenha(login: string, senha: string, senhaAntiga: string): Observable<object>{
    let r = new AlterarSenhaRequest(login, senha, senhaAntiga);

    return this.httpClient.post(this.httpSettings.ServerUrl + "auth/Autenticacao/alterar-senha", r, 
    this.httpSettings.RetHttpRequestDefaultOptions())
    .pipe(
      catchError(e => { return this.httpSettings.HandleHttpError(e) })
    )
  }

  constructor(private httpClient: HttpClient, private router: Router, 
    private persistenceService: PersistenceService, private httpSettings: HttpGlobalSettingsService) { 
    this._token = null;
  }
}
