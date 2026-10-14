import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { ProdutoODT } from '../odts/produto-odt';
import { SalvarProdutoRequest } from '../requests/salvar-produto-request';

@Injectable({ providedIn: 'root' })
export class ProdutosService {
    private produtosUrl = 'produtos';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    ListarProdutos(): Observable<ProdutoODT[]> {
        return this.httpClient.get<ProdutoODT[]>(this.httpSettingsService.ServerUrl + this.produtosUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterProduto(codProduto: number): Observable<ProdutoODT> {
        return this.httpClient.get<ProdutoODT>(this.httpSettingsService.ServerUrl + this.produtosUrl + '/' + codProduto)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    SalvarProduto(model: SalvarProdutoRequest): Observable<ProdutoODT> {
        return this.httpClient.post<ProdutoODT>(this.httpSettingsService.ServerUrl + this.produtosUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ExcluirProduto(codProduto: number): Observable<any> {
        return this.httpClient.delete(this.httpSettingsService.ServerUrl + this.produtosUrl + '/' + codProduto)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
