import { HttpClient, HttpErrorResponse, HttpHandler, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { AutenticaService } from '../../autenticacao/services/autentica.service';

@Injectable({
  providedIn: 'root'
})
export class ArquivosService {
  private url: string = "arquivos";

  constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService,
    private autenticaService: AutenticaService, private arquivosService: ArquivosService ) { }

  public Url(): string{
    return this.httpSettingsService.ServerUrl + this.url;
  }

  public ExcluirArquivo(codArquivo: number): Observable<any>{
    return this.httpClient.delete<any>(this.httpSettingsService.ServerUrl + this.url + "/" + codArquivo.toString())
    .pipe(catchError(e => this.httpSettingsService.HandleHttpError(e)));
  }
}
