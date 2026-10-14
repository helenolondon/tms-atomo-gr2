import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { ProdutoGrupoODT } from '../odts/produto-grupo-odt';
import { SalvarProdutoGrupoRequest } from '../requests/salvar-produto-grupo-request';

@Injectable({ providedIn: 'root' })
export class ProdutoGruposService {
    private produtoGruposUrl = 'produtogrupos';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    ListarProdutoGrupos(): Observable<ProdutoGrupoODT[]> {
        return this.httpClient.get<ProdutoGrupoODT[]>(this.httpSettingsService.ServerUrl + this.produtoGruposUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterProdutoGrupo(codProdutoGrupo: number): Observable<ProdutoGrupoODT> {
        return this.httpClient.get<ProdutoGrupoODT>(
            this.httpSettingsService.ServerUrl + this.produtoGruposUrl + '/' + codProdutoGrupo)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    SalvarProdutoGrupo(model: SalvarProdutoGrupoRequest): Observable<ProdutoGrupoODT> {
        return this.httpClient.post<ProdutoGrupoODT>(this.httpSettingsService.ServerUrl + this.produtoGruposUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ExcluirProdutoGrupo(codProdutoGrupo: number): Observable<any> {
        return this.httpClient.delete(
            this.httpSettingsService.ServerUrl + this.produtoGruposUrl + '/' + codProdutoGrupo)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
