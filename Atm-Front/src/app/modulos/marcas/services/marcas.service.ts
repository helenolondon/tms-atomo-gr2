import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpGlobalSettingsService } from 'src/app/infra/services/http-global-settings.service';
import { MarcaODT } from '../odts/marca-odt';
import { SalvarMarcaRequest } from '../requests/salvar-marca-request';

@Injectable({ providedIn: 'root' })
export class MarcasService {
    private marcasUrl = 'marcas';

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService) { }

    ListarMarcas(): Observable<MarcaODT[]> {
        return this.httpClient.get<MarcaODT[]>(this.httpSettingsService.ServerUrl + this.marcasUrl)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ObterMarca(codMarca: number): Observable<MarcaODT> {
        return this.httpClient.get<MarcaODT>(this.httpSettingsService.ServerUrl + this.marcasUrl + '/' + codMarca)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    SalvarMarca(model: SalvarMarcaRequest): Observable<MarcaODT> {
        return this.httpClient.post<MarcaODT>(this.httpSettingsService.ServerUrl + this.marcasUrl, model)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }

    ExcluirMarca(codMarca: number): Observable<any> {
        return this.httpClient.delete(this.httpSettingsService.ServerUrl + this.marcasUrl + '/' + codMarca)
            .pipe(catchError((e: HttpErrorResponse) => this.httpSettingsService.HandleHttpError(e)));
    }
}
