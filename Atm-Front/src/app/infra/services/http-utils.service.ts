/**
 * Autor: Heleno Sales Mesquita
 * Data : 16/02/2020
 * 
 * Serviço que fornece as opções globais para as requisições http.
 */
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpErrorResponse, HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { HttpError } from './models/HttpError';
import { Observable, throwError } from 'rxjs';

import { DialogsService } from './dialogs.service';
import { isNullOrUndefined } from 'util';
import { AutenticaService } from 'src/app/modulos/autenticacao/services/autentica.service';
import { HttpGlobalSettingsService } from './http-global-settings.service';
import { SystemService } from './system.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpUtilsService {
  constructor(private dialogs: DialogsService, private httpClient: HttpClient,
     private httpGlobalSettings: HttpGlobalSettingsService, private autenticaService: AutenticaService) { 
  }

  get<T>(url: string, parameters: [{key: string; value: string | number | boolean}] | null = null): Observable<T> | null{
    return this.httpClient.get<T>(this.httpGlobalSettings.ServerUrl + url, { params: this.getHttpParamsFromParameters(parameters), 
      headers: this.getDefaultHeaders() });
  }

  post<T extends Object>(url: string, body: any | null): Observable<T>;
  
  post(url: string, body: any | null): Observable<Object| unknown>;
  post(url: string, body: any | null): Observable<unknown>{
    return this.httpClient.post(this.httpGlobalSettings.ServerUrl + url, body)
    .pipe(
      catchError(e => { return this.httpGlobalSettings.HandleHttpError(e) })
    );
  };
  
  private getDefaultHeaders(){
    return this.httpGlobalSettings.RetHttpRequestDefaultOptions().headers.append("token", this.autenticaService.Token);
  }

  private getHttpParamsFromParameters(parameters: [{key: string; value: string | number | boolean}]): HttpParams {
    let pars = new HttpParams();

    if(parameters != null){
      parameters.forEach( p => {
        
        if(p.key == "token"){
          return
        };

        pars = pars.append(p.key, p.value.toString());
      });
    };

    return pars;
  }
}
