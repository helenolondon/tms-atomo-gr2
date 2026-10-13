import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { TipoVeiculoODT } from '../odts/tipo-veiculo-odt';
import { SalvarTipoVeiculoRequest } from '../requests/salvar-tipo-veiculo-request';

@Injectable({ providedIn: 'root' })
export class TiposVeiculoService {
    private tiposVeiculoUrl = 'tipoveiculo';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    Listar(): Observable<TipoVeiculoODT[]> {
        return this.httpClient.get<TipoVeiculoODT[]>(
            this.httpSettingsService.ServerUrl + this.tiposVeiculoUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterPorId(codTipoVeiculo: number): Observable<TipoVeiculoODT> {
        return this.httpClient.get<TipoVeiculoODT>(
            this.httpSettingsService.ServerUrl + this.tiposVeiculoUrl + '/' + codTipoVeiculo)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    Salvar(model: SalvarTipoVeiculoRequest): Observable<TipoVeiculoODT> {
        return this.httpClient.post<TipoVeiculoODT>(
            this.httpSettingsService.ServerUrl + this.tiposVeiculoUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    Excluir(codTipoVeiculo: number): Observable<any> {
        return this.httpClient.delete(
            this.httpSettingsService.ServerUrl + this.tiposVeiculoUrl + '/' + codTipoVeiculo)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
