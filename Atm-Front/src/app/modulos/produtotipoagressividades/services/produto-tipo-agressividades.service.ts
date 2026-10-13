import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { ProdutoTipoAgressividadeODT } from '../odts/produto-tipo-agressividade-odt';
import { SalvarProdutoTipoAgressividadeRequest } from '../requests/salvar-produto-tipo-agressividade-request';

@Injectable({ providedIn: 'root' })
export class ProdutoTipoAgressividadesService {
    private produtoTipoAgressividadeUrl = 'produtotipoagressividade';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    Listar(): Observable<ProdutoTipoAgressividadeODT[]> {
        return this.httpClient.get<ProdutoTipoAgressividadeODT[]>(
            this.httpSettingsService.ServerUrl + this.produtoTipoAgressividadeUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterPorId(codProdutoTipoAgressividade: number): Observable<ProdutoTipoAgressividadeODT> {
        return this.httpClient.get<ProdutoTipoAgressividadeODT>(
            this.httpSettingsService.ServerUrl + this.produtoTipoAgressividadeUrl + '/' + codProdutoTipoAgressividade)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    Salvar(model: SalvarProdutoTipoAgressividadeRequest): Observable<ProdutoTipoAgressividadeODT> {
        return this.httpClient.post<ProdutoTipoAgressividadeODT>(
            this.httpSettingsService.ServerUrl + this.produtoTipoAgressividadeUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    Excluir(codProdutoTipoAgressividade: number): Observable<any> {
        return this.httpClient.delete(
            this.httpSettingsService.ServerUrl + this.produtoTipoAgressividadeUrl + '/' + codProdutoTipoAgressividade)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
