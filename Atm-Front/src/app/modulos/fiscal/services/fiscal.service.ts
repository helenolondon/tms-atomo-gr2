import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { LookupItemResponse } from '../../compartilhado/LookupItemResponse';
import { CnaeResponse } from './responses/CnaeResponse';

@Injectable({
  providedIn: 'root'
})
export class FiscalService {
  private fiscalUrl: string = "fiscal/";

  constructor(private httpSettings: HttpGlobalSettingsService, private httpClient: HttpClient) { }

  public RetListaCnaes(q: string): Observable<CnaeResponse[]> {    
    let pars = new HttpParams();
    pars = pars.append('filtro', q);
    
    return this.httpClient
    .get<CnaeResponse[]>(this.httpSettings.ServerUrl + this.fiscalUrl + "cnaes", { params: pars } )
    .pipe(
      catchError( (e) => { return this.httpSettings.HandleHttpError( e ) } )
    );      
  }

  ListarSituacoesInscricaoEstadual(){
    return this.httpClient.get<LookupItemResponse[]>(this.httpSettings.ServerUrl + this.fiscalUrl + 
        "inscricao-estadual/situacoes");
  }
}