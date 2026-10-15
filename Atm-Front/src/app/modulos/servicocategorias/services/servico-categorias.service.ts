import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { SalvarServicoCategoriaRequest } from '../requests/salvar-servico-categoria-request';
import { ServicoCategoriaODT } from '../odts/servico-categoria-odt';

@Injectable({ providedIn: 'root' })
export class ServicoCategoriasService {
    private servicoCategoriasUrl = 'servicocategorias';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    ListarServicoCategorias(): Observable<ServicoCategoriaODT[]> {
        return this.httpClient.get<ServicoCategoriaODT[]>(
            this.httpSettingsService.ServerUrl + this.servicoCategoriasUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterServicoCategoria(codServicoCategoria: number): Observable<ServicoCategoriaODT> {
        return this.httpClient.get<ServicoCategoriaODT>(
            this.httpSettingsService.ServerUrl + this.servicoCategoriasUrl + '/' + codServicoCategoria)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    SalvarServicoCategoria(model: SalvarServicoCategoriaRequest): Observable<ServicoCategoriaODT> {
        return this.httpClient.post<ServicoCategoriaODT>(
            this.httpSettingsService.ServerUrl + this.servicoCategoriasUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ExcluirServicoCategoria(codServicoCategoria: number): Observable<any> {
        return this.httpClient.delete(
            this.httpSettingsService.ServerUrl + this.servicoCategoriasUrl + '/' + codServicoCategoria)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
