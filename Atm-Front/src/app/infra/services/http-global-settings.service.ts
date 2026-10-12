/**
 * Autor: Heleno Sales Mesquita
 * Data : 16/02/2020
 * 
 * Serviço que fornece as opções globais para as requisições http.
 */
import { Injectable, isDevMode } from '@angular/core';
import { HttpHeaders, HttpErrorResponse, HttpClient, HttpParams } from '@angular/common/http';
import { HttpError } from './models/HttpError';
import { Observable, throwError } from 'rxjs';

import { DialogsService } from '././dialogs.service';
import { isNullOrUndefined } from 'util';
import { AutenticaService } from 'src/app/modulos/autenticacao/services/autentica.service';

@Injectable({
  providedIn: 'root'
})
export class HttpGlobalSettingsService {

  get IsDebug(): boolean{
    return isDevMode();
  }

  get getServerDomain(): string {
    if(this.IsDebug){
      return  `localhost`;
    } else {
      return `hatom.com.br`;
    }
  }

  /**
   * Endereço do servidor
  */
  get ServerUrl(): string{
    if(this.IsDebug){
      return  `http://localhost/atmapicore/api/`;
    } else {
      return `https://abacos.api.hatom.com.br/api/`;
    }
  };

  /**
   * Constroi o cabeçalho padrão para requisições Http
   */
  public RetHttpRequestDefaultOptions(){
    return {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
  }

  /**
   * Tratamento de erros de baixo nível para requisições de http
   */
  HandleHttpError(error: HttpErrorResponse) {
    let temp = new HttpError(error.error instanceof ErrorEvent, error);
    
    if (temp.eClientSide) {
      return throwError('An error occurred: ', error.error.message);
    }
    else{
      
      if(!isNullOrUndefined(error.status) && error.status == 0){
        this.dialogs.showErroDeConexao("Houve um erro na comunicação com o servidor");
        temp.setErroTratado(true);
      }

      if(error.status == 501){
        this.dialogs.showErroHttp501(error.statusText);
        temp.setErroTratado(true);
      }
      
      if(error.status == 401){
        this.dialogs.erro("Não autorizado");
        temp.setErroTratado(true);
      }
      
      return throwError(temp);
    };
  };

  constructor(private dialogs: DialogsService) { 
  }
}
