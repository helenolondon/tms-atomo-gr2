import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { ProdutoTipoODT } from '../odts/produto-tipo-odt';
import { SalvarProdutoTipoRequest } from '../requests/salvar-produto-tipo-request';

@Injectable({ providedIn: 'root' })
export class ProdutoTiposService {
    private produtoTiposUrl = 'produtotipos';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    ListarProdutoTipos(): Observable<ProdutoTipoODT[]> {
        return this.httpClient.get<ProdutoTipoODT[]>(this.httpSettingsService.ServerUrl + this.produtoTiposUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterProdutoTipo(codProdutoTipo: number): Observable<ProdutoTipoODT> {
        return this.httpClient.get<ProdutoTipoODT>(this.httpSettingsService.ServerUrl + this.produtoTiposUrl + '/' + codProdutoTipo)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    SalvarProdutoTipo(model: SalvarProdutoTipoRequest): Observable<ProdutoTipoODT> {
        return this.httpClient.post<ProdutoTipoODT>(this.httpSettingsService.ServerUrl + this.produtoTiposUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ExcluirProdutoTipo(codProdutoTipo: number): Observable<any> {
        return this.httpClient.delete(this.httpSettingsService.ServerUrl + this.produtoTiposUrl + '/' + codProdutoTipo)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
