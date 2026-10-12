import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { HttpGlobalSettingsService } from "src/app/infra/services/http-global-settings.service";
import { EmpresaODT } from "../odts/empresa-odt";
import { SalvarEmpresaRequest } from "../requests/salvar-empresa-request";

@Injectable({providedIn: "root"})
export class EmpresasService{
    private empresasUrl: string = "empresas";

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService ) { }

    ObterEmpresa(codEmpresa: number): Observable<EmpresaODT>{
        return this.httpClient.get<EmpresaODT>(this.httpSettingsService.ServerUrl + this.empresasUrl + 
        "/" + codEmpresa)
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };
    
    ExcluirEmpresa(codEmpresa: number): Observable<any>{
        return this.httpClient.delete(this.httpSettingsService.ServerUrl + this.empresasUrl + 
        "/" + codEmpresa)
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };
    
    ListarEmpresas(razaoSocial: string = null, nomeFantasia: string = null, cnpj: string = null, responsavel: string = null, 
        inscricaoEstadual: string = null, telefone: string = null, email: string = null): Observable<EmpresaODT[]>{
        
        let pars = new HttpParams();
        
        if(razaoSocial != null){
            pars = pars.append("razaoSocial", razaoSocial);
        }

        if(nomeFantasia != null){
            pars = pars.append("nomeFantasia", nomeFantasia);
        }

        if(cnpj != null){
            pars = pars.append("cnpj", cnpj);
        }

        if(responsavel != null){
            pars = pars.append("responsavel", responsavel);
        }

        if(inscricaoEstadual != null){
            pars = pars.append("inscricaoEstadual", inscricaoEstadual);
        }

        if(razaoSocial != null){
            pars = pars.append("razaoSocial", razaoSocial);
        }

        if(telefone != null){
            pars = pars.append("telefone", telefone);
        }
        
        if(email != null){
            pars = pars.append("email", email);
        }

        return this.httpClient.get<EmpresaODT[]>(this.httpSettingsService.ServerUrl + this.empresasUrl, 
            { params: pars })
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };

    SalvarEmpresa(model: SalvarEmpresaRequest): Observable<EmpresaODT>{
        return this.httpClient.post<EmpresaODT>(this.httpSettingsService.ServerUrl + this.empresasUrl, model)
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };
}