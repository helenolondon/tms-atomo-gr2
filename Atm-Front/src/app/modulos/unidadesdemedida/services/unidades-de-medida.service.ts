import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { UnidadeDeMedidaODT } from '../odts/unidade-de-medida-odt';
import { SalvarUnidadeDeMedidaRequest } from '../requests/salvar-unidade-de-medida-request';

@Injectable({ providedIn: 'root' })
export class UnidadesDeMedidaService {
    private unidadesDeMedidaUrl = 'unidadesdemedida';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    Listar(): Observable<UnidadeDeMedidaODT[]> {
        return this.httpClient.get<UnidadeDeMedidaODT[]>(
            this.httpSettingsService.ServerUrl + this.unidadesDeMedidaUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterPorId(codUnidadeDeMedida: number): Observable<UnidadeDeMedidaODT> {
        return this.httpClient.get<UnidadeDeMedidaODT>(
            this.httpSettingsService.ServerUrl + this.unidadesDeMedidaUrl + '/' + codUnidadeDeMedida)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    Salvar(model: SalvarUnidadeDeMedidaRequest): Observable<UnidadeDeMedidaODT> {
        return this.httpClient.post<UnidadeDeMedidaODT>(
            this.httpSettingsService.ServerUrl + this.unidadesDeMedidaUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    Excluir(codUnidadeDeMedida: number): Observable<any> {
        return this.httpClient.delete(
            this.httpSettingsService.ServerUrl + this.unidadesDeMedidaUrl + '/' + codUnidadeDeMedida)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
