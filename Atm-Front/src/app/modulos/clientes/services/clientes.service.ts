import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ok } from "assert";
import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { HttpGlobalSettingsService } from "src/app/infra/services/http-global-settings.service";
import { LookupItemResponse } from "../../compartilhado/LookupItemResponse";
import { ListarClientesRequest } from "../requests/listar-clientes-request";
import { SalvarClienteRequest } from "../requests/salvar-cliente-request";
import { ClienteResponse } from "../responses/cliente-response";
import { ListarClientesResponse } from "../responses/listar-clientes-response";

@Injectable({providedIn: "root"})
export class ClientesService{
    private clientesUrl: string = "clientes";

    constructor(private httpClient: HttpClient, private httpSettingsService: HttpGlobalSettingsService ) { }

    ObterClientePorCodigo(codCliente: number): Observable<ClienteResponse>{
        return this.httpClient.get<ClienteResponse>(this.httpSettingsService.ServerUrl + this.clientesUrl + 
        "/" + codCliente)
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };
    
    ExcluirCliente(codCliente: number): Observable<any>{
        return this.httpClient.delete(this.httpSettingsService.ServerUrl + this.clientesUrl + 
        "/" + codCliente)
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };
    
    ListarClientesPorRazaoSocialOuCpfCnpj(razaoSocialOuCnpfCnpj: string): Observable<ListarClientesResponse[]>{
        let pars = new HttpParams();

        if(razaoSocialOuCnpfCnpj != null){
            pars = pars.append("razaoSocialOuCnpfCnpj", razaoSocialOuCnpfCnpj);
        }

        return this.httpClient.get<ListarClientesResponse[]>(this.httpSettingsService.ServerUrl + 
            this.clientesUrl + "/obter-por-razaosocial-ou-cpjcnpj", { params: pars })
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    }        

    ListarClientes(contato: string = null, cpfCnpj: string = null, email: string = null, identidade: string = null, 
        inscricaoEstadual: string = null, nome: string = null, razaoSocial: string = null, telefone: string = null): Observable<ListarClientesResponse[]>{
        
        let pars = new HttpParams();
        
        if(contato != null){
            pars = pars.append("contato", contato);
        }

        if(cpfCnpj != null){
            pars = pars.append("cpfCnpj", cpfCnpj);
        }

        if(identidade != null){
            pars = pars.append("identidade", identidade);
        }

        if(inscricaoEstadual != null){
            pars = pars.append("inscricaoEstadual", inscricaoEstadual);
        }

        if(nome != null){
            pars = pars.append("nome", nome);
        }

        if(razaoSocial != null){
            pars = pars.append("razaoSocial", razaoSocial);
        }

        if(telefone != null){
            pars = pars.append("telefone", telefone);
        }

        return this.httpClient.get<ListarClientesResponse[]>(this.httpSettingsService.ServerUrl + this.clientesUrl, 
            { params: pars })
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };

    SalvarCliente(model: SalvarClienteRequest): Observable<ClienteResponse>{
        return this.httpClient.post<ClienteResponse>(this.httpSettingsService.ServerUrl + this.clientesUrl, model)
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };

    ListarTiposDePessoa() : Observable<LookupItemResponse[]> {
        return this.httpClient.get<LookupItemResponse[]>(this.httpSettingsService.ServerUrl + this.clientesUrl + 
            "/tipos-pessoa")
        .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
    };

    ListarEstadosCivis(){
        return this.httpClient.get<LookupItemResponse[]>(this.httpSettingsService.ServerUrl + this.clientesUrl + 
            "/estados-civis")
            .pipe(catchError((e: HttpErrorResponse) => { return this.httpSettingsService.HandleHttpError(e)}));
        };

    private transformar(response: ClienteResponse[]){
        if(response != null){

        }

        return [];
    }
}