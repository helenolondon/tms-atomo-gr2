/**
 * Autor: Heleno Sales Mesquita
 * Data : 16/02/2020
 * 
 * Web services de usuários.
 */
import { Injectable } from '@angular/core';
import { UsuarioResponse } from '../responses/usuario-response';
import { Observable } from 'rxjs';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { SalvarUsuarioRequest } from '../resquests/SalvarUsuarioRequest';
import { RegistrarUsuarioRequest } from '../resquests/RegistrarUsuarioRequest';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService  {

  /**
   * Busca um usuário no servidor pelo código
   */
  public RetUsuarioPorCodigo(codigo: number){
    
    return this.httpClient
    .get<UsuarioResponse[]>(this.httpSettings.ServerUrl + "usuarios/" + codigo.toString() )
    .pipe(
      catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
    );      
  }

  /**
   * Retorna uma lista com todos os usuários do sistema, ativos ou não.
  */
  public RetUsarios(nome: string | null, email: string | null, login: string | null,
    telefone: string | null): Observable<UsuarioResponse[]> {
      
      let pars = new HttpParams();
      
      if(nome){
        pars = pars.append('nome', nome);
      }
      
      if(login){
        pars = pars.append('login', login)
      }
      
      if(email){
        pars = pars.append('email', email);
      }
      
      if(telefone){
        pars = pars.append('telefone', telefone);
      }

      return this.httpClient
      .get<UsuarioResponse[]>(this.httpSettings.ServerUrl + "usuarios", { params: pars })
      .pipe(
        catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
      );      
  }
  
  /**
   * Salva usuário
  */
 public SalvaUsuario(request: SalvarUsuarioRequest): Observable<any> {

  let url = request.id > 0 ? this.httpSettings.ServerUrl + "usuarios": 
    this.httpSettings.ServerUrl + "usuarios/novo-usuario";
  
  return this.httpClient
    .post(url, request)
    .pipe(
      catchError( (e) => { 
        return this.httpSettings.HandleHttpError( e ) 
      })
    );      
  }

  public RegistrarUsuario(request: RegistrarUsuarioRequest): Observable<any> {

    let url = this.httpSettings.ServerUrl + "usuarios/registrar";
    
    return this.httpClient
      .post(url, request)
      .pipe(
        catchError( (e) => { 
          return this.httpSettings.HandleHttpError( e ) 
        })
      );      
    }
  
  /**
   * Altera senha
   */

  public AlterarSenha(login: string, senha: string, novaSenha: string, confirmaSenha: string){
    let url = this.httpSettings.ServerUrl + "auth/autenticacao/alterar-senha";
    
    let body = {
      login: login,
      novaSenha: novaSenha,
      senha: senha,
      confirmaSenha: confirmaSenha
    };

    return this.httpClient
    .post(url, body, {responseType: 'text'})
    .pipe(
      catchError( (e) => { 
        return this.httpSettings.HandleHttpError( e ) 
      })
    );  
  }

  /**
   * Exclui usuário
   */
  public ExcluirUsuario(codigo: number){
    return this.httpClient
    .delete(this.httpSettings.ServerUrl + "usuarios/" + codigo.toString() )
    .pipe(
      catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
    );      
  }

  constructor(private httpClient: HttpClient, private httpSettings: HttpGlobalSettingsService) {
  }
}
